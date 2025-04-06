using DiscordBot.Core;
using DiscordBot.Core.Text;

namespace DiscordBot.Host.Handlers
{
    public class CommandMapper: ICommandMapper
    {
        private readonly Dictionary<string, ITextCommand> _commandToServiceMap;

        public CommandMapper(IEnumerable<ITextCommand> services)
        {
            _commandToServiceMap = services
                .SelectMany(service => service.GetSupportedCommands()
                    .Select(cmd => (cmd, service)))
                .ToDictionary(x => x.cmd, x => x.service);
        }

        public ITextCommand? GetServiceForCommand(string command) => _commandToServiceMap.GetValueOrDefault(command);
    }
}
