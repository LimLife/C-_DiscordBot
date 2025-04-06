using DiscordBot.Core.Text;

namespace DiscordBot.Core
{
    public interface ICommandMapper
    {
        ITextCommand? GetServiceForCommand(string command);
    }
}
