using DiscordBot.Core;

namespace DiscordBot.Host.Handlers
{
    public class CommandMapper: ICommandMapper
    {
        private readonly Dictionary<string, IServiceText> _commandToServiceMap;

        public CommandMapper(IEnumerable<IServiceText> services)
        {
            _commandToServiceMap = services
                .SelectMany(service => service.GetSupportedCommands()
                    .Select(cmd => (cmd, service)))
                .ToDictionary(x => x.cmd, x => x.service);
        }

        public IServiceText? GetServiceForCommand(string command) => _commandToServiceMap.GetValueOrDefault(command);
    }
}
