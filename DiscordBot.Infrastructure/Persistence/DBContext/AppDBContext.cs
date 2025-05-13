using DiscordBot.Core.Entity.Users;
using Microsoft.EntityFrameworkCore;
using DiscordBot.Infrastructure.Persistence.Configurations.Users;

namespace DiscordBot.Infrastructure.Persistence.DBContext
{
    public class AppDBContext : DbContext
    {
        public  DbSet<BaseUser> BaseUser {  get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BaseUserConfigurations());
        }
    }
}