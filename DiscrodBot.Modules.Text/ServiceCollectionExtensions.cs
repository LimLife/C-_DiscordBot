using Microsoft.Extensions.DependencyInjection;
using DiscordBot.Modules.Text.Command;
using DiscordBot.Text.Command;
using DiscordBot.Core;

namespace DiscordBot.Text.Extensions
{
    public static class DiscordServiceExtensions
    {
        public static IServiceCollection AddDiscordTextCommandHandlers(this IServiceCollection services)
        {
            services.AddSingleton<IServiceText, PingCommand>();
            services.AddSingleton<IServiceText, HelloWorldCommand>();
            services.AddSingleton<IServiceText, DotaCommand>();
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
