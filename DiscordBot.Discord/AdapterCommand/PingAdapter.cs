using DiscordBot.Core.Commands;
using Discord.Interactions;



namespace DiscordBot.Discord.AdapterCommand
{
    public class PingAdapter : InteractionModuleBase<SocketInteractionContext>
    {
        public readonly IPingCommand _pingCommand;
        public PingAdapter(IPingCommand pingCommand) => _pingCommand = pingCommand;

        [SlashCommand(IPingCommand.CommandName, IPingCommand.CommandDescription)]
        public async Task PingCommandAsync()
        {
            var response = await _pingCommand.ResponseAsync();
            await ReplyAsync(response);
        }
    }
}
