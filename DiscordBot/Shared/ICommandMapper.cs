using DiscordBot.Core.Text;

namespace DiscordBot.Core
{
    public interface ICommandMapper
    {
        IServiceText? GetServiceForCommand(string command);
    }
}
