using DiscordBot.Core.Entity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DiscordBot.Infrastructure.Persistence.Configurations.ConfigurationsName;

namespace DiscordBot.Infrastructure.Persistence.Configurations.Users
{
    public class BaseUserConfigurations : IEntityTypeConfiguration<BaseUser>
    {
        public void Configure(EntityTypeBuilder<BaseUser> builder)
        {
            builder.ToTable(DbConstants.Tables.BaseUser, DbConstants.Schema.Users);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.Email);
            builder.Property(x => x.Password);
            builder.Property(x=> x.UserName);
        }
    }
}
