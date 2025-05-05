using DiscordBot.Discord.MessageContext;
using Discord.WebSocket;
using DiscordBot.Core;


namespace DiscordBot.Host.Handlers
{
    public class MessageHandler
    {
        private readonly ICommandMapper _commandMapper;
        public MessageHandler(ICommandMapper commandMapper) => _commandMapper = commandMapper;

        public async Task HandleMessageAsync(SocketMessage message)
        {
            if (message is not SocketUserMessage userMessage || message.Author.IsBot)
                return;
            string command = message.Content.ToLower();
            var service = _commandMapper.GetServiceForCommand(command);
          /*  if (service is not null)
                await service.ExecuteAsync(command, new DiscordMessageContext(message));
          */
        }
    }
}
