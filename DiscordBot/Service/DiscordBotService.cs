using Discord;
using Discord.WebSocket;
using DiscordBot.Host.Config;
using DiscordBot.Host.Handlers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DiscordBot.Host.Service
{
    public class DiscordBotService : BackgroundService
    {
        private readonly ILogger<DiscordBotService> _logger;
        private readonly MessageHandler _messageHandler;
        private readonly DiscordSocketClient _client;
        private readonly LogHandler _logHandler;
        private readonly BotConfig _config;

        public DiscordBotService(BotConfig config, ILogger<DiscordBotService> logger, LogHandler logHandler, MessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
            _logHandler = logHandler;
            _config = config;
            _logger = logger;

            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = _config.Intents,
                LogLevel = _config.LogLevel
            });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _client.Log += _logHandler.HandleLogAsync;
            _client.MessageReceived += _messageHandler.HandleMessageAsync;
            _client.Ready += OnReadyAsync;

            try
            {
                await _client.LoginAsync(TokenType.Bot, _config.Token);
                await _client.StartAsync();
                _logger.LogInformation("Discord bot started successfully");

                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to start Discord bot");
                throw;
            }
            finally
            {
                await StopAsync(stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping Discord bot...");
            _client.Log -= _logHandler.HandleLogAsync;
            _client.MessageReceived -= _messageHandler.HandleMessageAsync;
            _client.Ready -= OnReadyAsync;
            _logger.LogInformation("Discord bot stopped");

            if (_client.LoginState == LoginState.LoggedIn)
            {
                await _client.StopAsync().ConfigureAwait(false);
                await _client.LogoutAsync().ConfigureAwait(false);
            }
        }
        private async Task OnReadyAsync()
        {
            _logger.LogInformation($"Bot connected as {_client.CurrentUser?.Username}");
            await _client.SetActivityAsync(new Game("Type !help"));
        }
    }
}