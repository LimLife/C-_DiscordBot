using DiscordBot.Core;
using DiscordBot.Core.Text;
using DiscordBot.Modules.Text.Command;
using DiscordBot.Text.Command;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot.Text.Extensions
{
    public static class DiscordServiceExtensions
    {
        public static IServiceCollection AddDiscordCommandHandlers(this IServiceCollection services)
        {
            services.AddSingleton<IServiceText, PingCommand>();
            services.AddSingleton<IServiceText, HelloWorldCommand>();
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
