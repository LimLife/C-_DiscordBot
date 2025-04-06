using DiscordBot.Core.Text;
using DiscordBot.Modules.Text.Command;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot.Text.Extensions
{
    public static class DiscordServiceExtensions
    {
        public static IServiceCollection AddDiscordCommandHandlers(this IServiceCollection services)
        {
            services.AddTransient<ITextCommand, PingCommand>();
            return services;
        }

        /*
        public static IServiceCollection AddDiscordInfrastructure(this IServiceCollection services)
        {
            
            return services;
        }
        */
    }

}
