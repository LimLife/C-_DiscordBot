using Microsoft.Extensions.DependencyInjection;
using DiscordBot.NetworkKit.SteamCommand;
using DiscordBot.Core.REST;

namespace DiscordBot.NetworkKit
{
    public static class ServiceCollectionNetworkKit
    {
        public static IServiceCollection AddDotaRestClient( this IServiceCollection services, Action<HttpClient>? configureClient = null)
        {
            services.AddHttpClient<IDotaDiscordBridge, DotaDiscordBridge>(client =>
            {
                client.BaseAddress = new Uri("https://api.opendota.com/api/");
                configureClient?.Invoke(client);
            });
            return services;
        }
    }
}
