using Microsoft.Extensions.DependencyInjection;
using DiscordBot.Host.Handlers;
using DiscordBot.Core;

namespace DiscordBot.Host.ServiceConfigurations
{
    public static class HandlerRegistrations
    {
        /// <summary>
        /// Регистрирует обработчики хэндлеров, такие как 
        /// <see cref="CommandMapper"/> и другие.
        /// </summary>
        /// <param name="services">Сервис-контейнер DI</param>
        /// <returns>Обновлённый <see cref="IServiceCollection"/></returns>
        /// <remarks>
        /// Реализованные команды:
        /// <list type="bullet">
        ///   <item><see cref="ICommandMapper"/> → <see cref="CommandMapper"/></item>
        ///   <item><see cref="MessageHandler"/> → <see cref="MessageHandler"/></item>
        ///   <item><see cref="LogHandler"/> → <see cref="LogHandler"/></item>
        /// </list>
        /// </remarks>
        public static IServiceCollection AddDiscordHandlers(this IServiceCollection services)
        {
           // services.AddSingleton<ICommandMapper, CommandMapper>();
           // services.AddSingleton<MessageHandler>();
            services.AddSingleton<LogHandler>();
            return services;
        }
    }
}
