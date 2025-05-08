using DiscordBot.Infrastructure.Persistence.DBContext;
using Microsoft.Extensions.DependencyInjection;
using DiscordBot.Host.ServiceConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using DiscordBot.Text.Extensions;
using DiscordBot.Infrastructure;
using DiscordBot.Host.Config;
using DiscordBot.NetworkKit;
using Discord.Interactions;
using DiscordBot.Discord;
using Discord.WebSocket;
using Discord.Commands;
using DotNetEnv;
using Discord;



Env.TraversePath().Load(".env.dev");

var builder = new HostApplicationBuilder(args);

builder.Services.AddSingleton(new BotConfig
{
    Token = Env.GetString("DISCORD_TOKEN") ?? throw new ArgumentNullException("DISCORD_TOKEN is not configured"),
    GuildID = ulong.Parse(Env.GetString("DISCORD_GUILD")),
    Intents = GatewayIntents.Guilds | GatewayIntents.GuildMessages | GatewayIntents.MessageContent,
    LogLevel = LogSeverity.Debug,
});
builder.Services.AddSingleton(new DataBaseConfig
{
    Host = Env.GetString("DB_HOST") ?? "test-host",
    DBName = Env.GetString("DB_NAME") ?? "test-userName",
    Password = Env.GetString("DB_PASSWORD") ?? "test-pass",
    Port = Env.GetString("DB_PORT") ?? "test-port",
    UserName = Env.GetString("DB_USERNAME") ?? "test-userName"

});
#region Infrastructure
builder.Services.AddPersistence(provider =>
{
    var config = provider.GetRequiredService<DataBaseConfig>();
    return config.GetConnectionString();
});
#endregion
builder.Services.AddSingleton<CommandService>();
builder.Services.AddSingleton(provider =>
{
    var config = provider.GetRequiredService<BotConfig>();
    return new DiscordSocketClient(new DiscordSocketConfig
    {
        GatewayIntents = config.Intents,
        LogLevel = config.LogLevel,
        UseInteractionSnowflakeDate = false
    });
}).AddSingleton(provider =>
{
    var client = provider.GetRequiredService<DiscordSocketClient>();
    return new InteractionService(client);
});

#region Application services
builder.Services.AddDiscordTextCommandHandlers();
#endregion

#region DiscordBot services 
builder.Services.AddDiscordCommand();
#endregion



#region NetworkKit
builder.Services.AddDotaRestClient();
#endregion

builder.Services.AddDiscordHandlers();
builder.Services.AddDiscordServices();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDBContext>();
    db.Database.Migrate();
}
await app.RunAsync();
