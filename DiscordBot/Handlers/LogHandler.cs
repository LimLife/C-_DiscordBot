using Discord;
using Microsoft.Extensions.Logging;

namespace DiscordBot.Host.Handlers
{
    public class LogHandler
    {
        private readonly ILogger<LogHandler> _logger;

        public LogHandler(ILogger<LogHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleLogAsync(LogMessage message)
        {
            var logLevel = message.Severity switch
            {
                LogSeverity.Critical => LogLevel.Critical,
                LogSeverity.Error => LogLevel.Error,
                LogSeverity.Warning => LogLevel.Warning,
                LogSeverity.Info => LogLevel.Information,
                LogSeverity.Verbose => LogLevel.Debug,
                LogSeverity.Debug => LogLevel.Trace,
                _ => LogLevel.Information
            };

            _logger.Log(logLevel, message.Exception, "[{Source}] {Message}", message.Source, message.Message);
            return Task.CompletedTask;
        }
    }
}
