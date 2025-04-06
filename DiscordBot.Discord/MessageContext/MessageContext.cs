
using Discord.WebSocket;
using DiscordBot.Core.Text;

namespace DiscordBot.Discord.MessageContext
{
    public class MessageContext : IMessageContext
    {
        private readonly SocketMessage _message;
        public string Content => _message.Content;

        public ulong ChannelId => _message.Channel.Id;

        public MessageContext(SocketMessage message) => _message = message;

        public async Task ReplyAsync(string message)
        {
            await _message.Channel.SendMessageAsync(message);
        }
    }
}
