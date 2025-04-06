
namespace DiscordBot.Core.Text
{
    public interface ITextCommand
    {
        IEnumerable<string> GetSupportedCommands();
        bool CanHandle(string command);
        Task ExecuteAsync(string command, IMessageContext context);
    }
}
