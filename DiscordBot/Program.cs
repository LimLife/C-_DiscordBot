using Microsoft.Extensions.DependencyInjection;
using DiscordBot.Host.ServiceConfigurations;
using Microsoft.Extensions.Hosting;
using DiscordBot.Text.Extensions;
using DiscordBot.Host.Config;
using DiscordBot.NetworkKit;
using DotNetEnv;
using Discord;

Env.TraversePath().Load();

var builder = new HostApplicationBuilder(args);

builder.Services.AddSingleton(new BotConfig
{
    Token = Env.GetString("DISCORD_TOKEN") ?? throw new ArgumentNullException("DISCORD_TOKEN is not configured"),
    Intents = GatewayIntents.Guilds | GatewayIntents.GuildMessages | GatewayIntents.MessageContent,
    LogLevel = LogSeverity.Debug
});
builder.Services.AddDiscordHandlers();
builder.Services.AddDiscordServices();
builder.Services.AddDiscordTextCommandHandlers();
builder.Services.AddDotaRestClient();

var app = builder.Build();
await app.RunAsync();
