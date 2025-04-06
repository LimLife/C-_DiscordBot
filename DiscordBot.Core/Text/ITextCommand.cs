
namespace DiscordBot.Core.Text
{
    public interface ITextCommand
    {
        Task ExecuteAsync(string command, IMessageContext context);
    }
}
