using DiscordBot.Core;
using DiscordBot.Core.REST;
using DiscordBot.Core.Text;

namespace DiscordBot.Text.Command
{
    public class DotaCommand : IServiceText
    {
        public IEnumerable<string> GetSupportedCommands() => _commands;

        private static readonly string[] _commands = { "!win", "!mmr"};
        private readonly IDotaDiscordBridge _dotaDiscordBridge;
        public DotaCommand(IDotaDiscordBridge dotaDiscordBridge) => _dotaDiscordBridge = dotaDiscordBridge;

        public async Task ExecuteAsync(string command, IMessageContext context)
        {
            switch (command)
            {
                case "!win": var s = await _dotaDiscordBridge.GetUserIsWinAsync(147953395);
                    await context.ReplyAsync(!s ? "false": "true");
                    break;
                case "!mmr": var mmr =  await _dotaDiscordBridge.GetUserRatingAsync(147953395);
                    await context.ReplyAsync(mmr?.ToString() ?? "exaption");
                    break;
                default: await Task.CompletedTask;
                    break;
            }
        }
    }
}
