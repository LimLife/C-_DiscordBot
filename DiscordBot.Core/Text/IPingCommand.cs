
namespace DiscordBot.Core.Text
{
    public interface IPingCommand
    {
       public Task<string> GetPingResponseAsync();
    }
}
