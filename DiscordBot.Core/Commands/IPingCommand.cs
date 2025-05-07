namespace DiscordBot.Core.Commands
{
    public interface IPingCommand : ICommand
    {
        public const string CommandName = "ping";
        public const string CommandDescription = "Ping description -_-";
    }
}
