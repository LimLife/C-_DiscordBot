using Microsoft.Extensions.DependencyInjection;
using DiscordBot.Text.Command;
using DiscordBot.Core;
using DiscordBot.Modules.Text.Command;
using DiscordBot.Core.Text;

namespace DiscordBot.Text.Extensions
{
    public static class DiscordServiceExtensions
    {
        /// <summary>
        /// Регистрирует обработчики текстовых команд, такие как 
        /// <see cref="PingCommand"/> и другие.
        /// </summary>
        /// <param name="services">Сервис-контейнер DI</param>
        /// <returns>Обновлённый <see cref="IServiceCollection"/></returns>
        /// <remarks>
        /// Реализованные команды:
        /// <list type="bullet">
        ///   <item><see cref="IPingCommand"/> → <see cref="PingCommand"/></item>
        ///   <item><see cref="IServiceText"/> → <see cref="HelloWorldCommand"/></item>
        ///   <item><see cref="IServiceText"/> → <see cref="DotaCommand"/></item>
        /// </list>
        /// </remarks>
        public static IServiceCollection AddDiscordTextCommandHandlers(this IServiceCollection services)
        {
            services.AddSingleton<IPingCommand, PingCommand>();
           // services.AddSingleton<IServiceText, HelloWorldCommand>();
           // services.AddSingleton<IServiceText, DotaCommand>();
            
            return services;
        }
    }

}
