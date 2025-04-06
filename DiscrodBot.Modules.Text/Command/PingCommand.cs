using DiscordBot.Core;
using DiscordBot.Core.Text;

namespace DiscordBot.Modules.Text.Command
{
    public class PingCommand : IServiceText
    {
        private static readonly string[] _commands = { "ping", "help", "status" };
        public IEnumerable<string> GetSupportedCommands() => _commands;

        public async Task ExecuteAsync(string command, IMessageContext context)
        {
            switch (command.ToLower())
            {
                case "ping":
                    await context.ReplyAsync("Pong");
                    break;
                default:
                    await context.ReplyAsync("Exaction message");
                    break;
            }
        }

    }
}
