using Microsoft.Extensions.DependencyInjection;
using DiscordBot.Host.ServiceConfigurations;
using Microsoft.Extensions.Hosting;
using DiscordBot.Text.Extensions;
using DiscordBot.Host.Config;
using DiscordBot.NetworkKit;
using Discord.Interactions;
using DiscordBot.Discord;
using Discord.WebSocket;
using Discord.Commands;
using DotNetEnv;
using Discord;



Env.TraversePath().Load();

var builder = new HostApplicationBuilder(args);
builder.Services.AddSingleton(new BotConfig
{
    Token = Env.GetString("DISCORD_TOKEN") ?? throw new ArgumentNullException("DISCORD_TOKEN is not configured"),
    GuildID = ulong.Parse(Env.GetString("DISCORD_GUILD")),
    Intents = GatewayIntents.Guilds | GatewayIntents.GuildMessages | GatewayIntents.MessageContent,
    LogLevel = LogSeverity.Debug,
});

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
//test Area

//End Test

#region Application services
builder.Services.AddDiscordTextCommandHandlers();
#endregion

#region DiscordBot services 
builder.Services.AddDiscordCommand();
#endregion

builder.Services.AddDiscordHandlers();
builder.Services.AddDotaRestClient();




builder.Services.AddDiscordServices();

var app = builder.Build();
await app.RunAsync();
