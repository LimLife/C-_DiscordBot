using Microsoft.Extensions.DependencyInjection;
using DiscordBot.NetworkKit.SteamCommand;
using DiscordBot.Core.REST;

namespace DiscordBot.NetworkKit
{
    public static class ServiceCollectionNetworkKit
    {
        /// <summary>
        /// Регистрирует обработчики работы с REST для работы с DOTA сервисами
        /// <see cref="DotaDiscordBridge"/> и другие.
        /// </summary>
        /// <param name="services">Сервис-контейнер DI</param>
        /// <param name="configureClient">
        /// Делегат, позволяющий настроить экземпляр <see cref="HttpClient"/> перед его использованием.
        /// </param>
        /// <returns>Обновлённый <see cref="IServiceCollection"/></returns>
        /// <remarks>
        /// Реализованные команды:
        /// <list type="bullet">
        ///   <item><see cref="IDotaDiscordBridge"/> → <see cref="DotaDiscordBridge"/></item>
        /// </list>
        /// </remarks>
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
