using Discord;
using Discord.Interactions;
using DiscordBot.Core.Text;



namespace DiscordBot.Discord.AdapterContext
{
    public class PingAdapterContext : InteractionModuleBase<SocketInteractionContext>
    {
        public readonly IPingCommand _pingCommand;
        public PingAdapterContext(IPingCommand pingCommand) => _pingCommand = pingCommand;

        [SlashCommand("test22222", "test21212")]
        public async Task PingCommandAsync()
        {
            var component = new ComponentBuilder()
            .WithButton("ping", "test", ButtonStyle.Success);
            var response = await _pingCommand.GetPingResponseAsync();
            await ReplyAsync(response, components: component.Build());
        }
    }
}
