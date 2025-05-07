using DiscordBot.Core.Commands;

namespace DiscordBot.Modules.Text.Command
{
    public class PingCommand : IPingCommand
    {
        public Task<string> ResponseAsync(string msg = "")
        {
            return Task.FromResult("Ping");
        }
    }
}
