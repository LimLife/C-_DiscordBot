using Microsoft.Extensions.DependencyInjection;
using DiscordBot.Discord.AdapterCommand;

namespace DiscordBot.Discord
{

    public static class DiscordServicesCollectionExtensions
    {
        /// <summary>
        /// Регистрирует обработчики текстовых команд через DI используя API Discord.Net, такие как 
        /// <see cref="PingAdapterContext"/> и другие.
        /// </summary>
        /// <param name="services">Сервис-контейнер DI</param>
        /// <returns>Обновлённый <see cref="IServiceCollection"/></returns>
        /// <remarks>
        /// Реализованные команды:
        /// <list type="bullet">
        ///   <item><see cref="PingAdapter"/> → <see cref="IPingCommand"/></item>
        ///   <item><see cref="HelperAdapter"/> → <see cref="IHelper"/></item>
        /// </list>
        /// </remarks>
        public static IServiceCollection AddDiscordCommand(this IServiceCollection services)
        {
            services.AddSingleton<PingAdapter>();
            services.AddSingleton<HelperAdapter>();
            return services;
        }
    }

}
