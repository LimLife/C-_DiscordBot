using DiscordBot.Infrastructure.Persistence.DBContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiscordBot.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, Func<IServiceProvider, string> connectionString)
        {
            services.AddDbContext<AppDBContext>((provider, options) =>
            {
                options.UseNpgsql(connectionString(provider));
            });
            return services;
        }
        public static IServiceCollection AddPersistenceDev(this IServiceCollection services, Func<IServiceProvider, string> connectionString)
        {
            services.AddDbContext<AppDBContext>((provider, options) =>
            {
                options.UseNpgsql(connectionString(provider)).EnableSensitiveDataLogging().LogTo(Console.WriteLine, LogLevel.Information);
            });
            return services;
        }
    }

}
