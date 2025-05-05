using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DiscordBot.Host.Handlers;
using DiscordBot.Host.Config;
using Discord.Interactions;
using System.Reflection;
using Discord.WebSocket;
using Discord;
using DiscordBot.Discord.AdapterContext;


namespace DiscordBot.Host.Service
{
    public class DiscordBotService : BackgroundService
    {
        private readonly ILogger<DiscordBotService> _logger;
        private readonly IServiceProvider _services;
        private readonly LogHandler _logHandler;
        private readonly BotConfig _config;
        private readonly DiscordSocketClient _client;
        private InteractionService _interactionService;
        public DiscordBotService(ILogger<DiscordBotService> logger, IServiceProvider services, LogHandler logHandler, BotConfig config)
        {
            _logger = logger;
            _services = services;
            _logHandler = logHandler;
            _config = config;

            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = _config.Intents,
                LogLevel = _config.LogLevel,
                UseInteractionSnowflakeDate = false
            });

            _interactionService = new InteractionService(_client);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _client.Ready += OnReadyAsync;
            _client.Log += _logHandler.HandleLogAsync;
            _client.InteractionCreated += HandleInteraction;
            _interactionService.Log += _logHandler.HandleLogAsync;
            _interactionService.SlashCommandExecuted += TestingSlashCommand;

            try
            {
                await _client.LoginAsync(TokenType.Bot, _config.Token);
                await _client.StartAsync();

                _logger.LogInformation("Discord bot started successfully");

                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(Timeout.Infinite, stoppingToken);
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

        private async Task TestingSlashCommand(SlashCommandInfo arg1, IInteractionContext ctx, IResult result)
        {
            if (!result.IsSuccess)
            {
                await ctx.Interaction.RespondAsync($"Ошибка: {result.ErrorReason}", ephemeral: true);
                _logger.LogError(result.ErrorReason);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping Discord bot...");

            _client.Log -= _logHandler.HandleLogAsync;

            _client.Ready -= OnReadyAsync;

            _logger.LogInformation("Discord bot stopped");

            _client.InteractionCreated -= HandleInteraction;

            _interactionService.SlashCommandExecuted -= TestingSlashCommand;
            _interactionService.Log -= _logHandler.HandleLogAsync;

            if (_client.LoginState == LoginState.LoggedIn)
            {
                await _client.StopAsync();
                await _client.LogoutAsync();
            }
        }

        private async Task OnReadyAsync()
        {
            _logger.LogInformation($"Bot connected as {_client.CurrentUser?.Username}");

            await _client.SetActivityAsync(new Game("Test Bot"));
            await _interactionService.AddModulesAsync(typeof(PingAdapterContext).Assembly, _services);
            await _interactionService.RegisterCommandsToGuildAsync(_config.GuildID, true);
        }

        private async Task HandleInteraction(SocketInteraction interaction)
        {
            var ctx = new SocketInteractionContext(_client, interaction);
            await _interactionService.ExecuteCommandAsync(ctx, _services);

            _logger.LogError("Guild reg");
        }
    }
}