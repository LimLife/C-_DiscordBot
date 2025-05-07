
namespace DiscordBot.Core.Commands
{
    public interface ICommand
    {
        public Task<string> ResponseAsync(string msg = "");
    }
}
