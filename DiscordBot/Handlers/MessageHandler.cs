using Discord.WebSocket;
using DiscordBot.Core;
using DiscordBot.Core.Text;
using DiscordBot.Discord.MessageContext;


namespace DiscordBot.Host.Handlers
{
    public class MessageHandler
    {
        private readonly ICommandMapper _commandMapper;
        public MessageHandler(ICommandMapper commandMapper)
        {
            _commandMapper = commandMapper;
        }
        public async Task HandleMessageAsync(SocketMessage message)
        {
            if (message is not SocketUserMessage userMessage || message.Author.IsBot)
                return;
            string command = message.Content.ToLower();
            var service = _commandMapper.GetServiceForCommand(command);
            if (service != null)
            {
                var context = new MessageContext(message);
                await service.ExecuteAsync(command, context);
            }
        }      
    }
}
