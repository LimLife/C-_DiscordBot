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
        private readonly DiscordSocketClient _client;
        private readonly BotConfig _config;
        private readonly ILogger<DiscordBotService> _logger;
        private readonly LogHandler _logHandler;
        private readonly MessageHandler _messageHandler;

        public DiscordBotService(BotConfig config, ILogger<DiscordBotService> logger, LogHandler logHandler, MessageHandler messageHandler)
        {
            _config = config;
            _logger = logger;
            _logHandler = logHandler;

            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = _config.Intents,
                LogLevel = _config.LogLevel
            });
            _messageHandler = messageHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _client.Log += _logHandler.HandleLogAsync;
            _client.MessageReceived += _messageHandler.HandleMessageAsync;
            try
            {
                await _client.LoginAsync(TokenType.Bot, _config.Token);
                await _client.StartAsync();
                _logger.LogInformation("Discord bot started successfully");

                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(1000, stoppingToken);
                }
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

            if (_client.LoginState == LoginState.LoggedIn)
            {
                await _client.StopAsync();
                await _client.LogoutAsync();
            }

            _client.Log -= _logHandler.HandleLogAsync;
            _client.MessageReceived -= _messageHandler.HandleMessageAsync;
            _logger.LogInformation("Discord bot stopped");
        }
    }
}