using Microsoft.Extensions.DependencyInjection;
using DiscordBot.Host.Service;

namespace DiscordBot.Host.ServiceConfigurations
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddDiscordServices(this IServiceCollection services)
        {
            services.AddHostedService<DiscordBotService>();
            return services;
        }
    }
}
