using Discord.Interactions;
using DiscordBot.Core.Text;

namespace DiscordBot.Discord.MessageContext
{
    public class DiscordMessageContext //: InteractionModuleBase<SocketInteractionContext>
    {
        private IMessageContext _messageContext;

        public DiscordMessageContext(IMessageContext messageContext) => _messageContext = messageContext;

        public async Task ReplyAsync(string message)
        {
            await ReplyAsync(message);
            // await _message.Channel.SendMessageAsync(message);
        }
    }
}
