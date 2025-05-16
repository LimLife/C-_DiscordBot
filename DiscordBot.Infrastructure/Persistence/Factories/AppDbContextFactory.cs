using DiscordBot.Infrastructure.Persistence.DBContext;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;



namespace DiscordBot.Infrastructure.Persistence.Factories
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDBContext>
    {

        public AppDBContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();

            var envPath = Path.Combine(basePath, "..", "DiscordBot", ".env.dev");

            envPath = Path.GetFullPath(envPath);
            if (!File.Exists(envPath))
            {
                throw new InvalidOperationException($"File not exists: {envPath}");
            }

            foreach (var line in File.ReadAllLines(envPath))
            {
                if (string.IsNullOrWhiteSpace(line) || line.Trim().StartsWith("#")) continue;
                var parts = line.Split('=', 2, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    Environment.SetEnvironmentVariable(parts[0].Trim(), parts[1].Trim());
                }
            }

            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbUser = Environment.GetEnvironmentVariable("DB_USERNAME");
            var dbPass = Environment.GetEnvironmentVariable("DB_PASSWORD");
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbPort = Environment.GetEnvironmentVariable("DB_PORT");

            if (string.IsNullOrWhiteSpace(dbName) || string.IsNullOrWhiteSpace(dbUser) || string.IsNullOrWhiteSpace(dbPass))
            {
                Console.WriteLine(dbName);
                throw new InvalidOperationException("Not found: DB_NAME, DB_USERNAME, DB_PASSWORD");
            }

            var connectionString = $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPass}";

            var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new AppDBContext(optionsBuilder.Options);
        }
    }
}
