
namespace DiscordBot.Core.Commands
{
    public interface IHelperCommand : ICommand
    {
        public const string CommandName = "help";
        public const string CommandDescription = "Show all command";
    }
}
