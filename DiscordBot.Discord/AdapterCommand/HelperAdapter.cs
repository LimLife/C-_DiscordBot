using DiscordBot.Core.Commands;
using Discord.Interactions;
using Discord;

namespace DiscordBot.Discord.AdapterCommand
{
    public class HelperAdapter : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly IHelperCommand _helper;
        private readonly InteractionService _interactionService;
        public HelperAdapter(IHelperCommand helper, InteractionService interactionService)
        {
            _helper = helper;
            _interactionService = interactionService;
        }

        [SlashCommand(IHelperCommand.CommandName, IHelperCommand.CommandDescription)]
        public async Task HelpCommandAsync()
        {
             var commands =  _interactionService.SlashCommands;
            var embedBuilder = new EmbedBuilder().WithTitle("📘 Доступные команды").WithColor(Color.Blue);

            foreach (var command in commands)
            {
                embedBuilder.AddField($"/{command.Name}", command.Description ,inline: false);
            }
           await RespondAsync(embed: embedBuilder.Build());
        }
    }
}
