using Microsoft.Extensions.DependencyInjection;
using DiscordBot.Host.ServiceConfigurations;
using Microsoft.Extensions.Hosting;
using DiscordBot.Text.Extensions;
using DiscordBot.Host.Config;
using DiscordBot.NetworkKit;
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

builder.Services.AddSingleton<DiscordSocketClient>();
builder.Services.AddSingleton<CommandService>();
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
