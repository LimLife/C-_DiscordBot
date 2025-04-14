using Microsoft.Extensions.DependencyInjection;
using DiscordBot.Host.Handlers;
using DiscordBot.Core;

namespace DiscordBot.Host.ServiceConfigurations
{
    public static class HandlerRegistrations
    {
        public static IServiceCollection AddDiscordHandlers(this IServiceCollection services)
        {
            services.AddSingleton<ICommandMapper, CommandMapper>();
            services.AddSingleton<MessageHandler>();
            services.AddSingleton<LogHandler>();
            return services;
        }
    }
}
