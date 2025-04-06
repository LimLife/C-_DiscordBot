
namespace DiscordBot.Core.Text
{
    public interface IMessageContext
    {
        string Content { get; }
        ulong ChannelId { get; }
        Task ReplyAsync(string message);
    }
}
