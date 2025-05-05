using DiscordBot.Core;
using DiscordBot.Core.Text;

namespace DiscordBot.Text.Command
{
    public class HelloWorldCommand : IServiceText
    {
        private static readonly string[] _commands = { "hi", "hello", "privet" };
        public IEnumerable<string> GetSupportedCommands() => _commands;

        public async Task ExecuteAsync(string command, IMessageContext context)
        {
            switch (command.ToLower())
            {
                case "hi":
                    await context.ReplyAsync("Привет");
                    break;
                case "hello":
                    await context.ReplyAsync("Привет дружок");
                    break;
                case "privet":
                    await context.ReplyAsync("Привет дружок пирожок");
                    break;
                default:
                    await context.ReplyAsync("Exaction message");
                    break;
            }
        }
    }
}
