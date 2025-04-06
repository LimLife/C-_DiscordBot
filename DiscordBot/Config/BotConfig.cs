using Discord;

namespace DiscordBot.Host.Config
{
    public class BotConfig
    {
        public string Token { get; set; }
        public GatewayIntents Intents { get; set; }
        public LogSeverity LogLevel { get; set; }
    }
}
