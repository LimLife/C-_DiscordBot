using Discord;
using DotNetEnv;
using DiscordBot.Host.Config;
using DiscordBot.Host.Service;
using DiscordBot.Host.Handlers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using DiscordBot.Text.Extensions;
using DiscordBot.Core;
using DiscordBot.Core.Text;
using DiscordBot.Modules.Text.Command;

Env.TraversePath().Load();

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton(new BotConfig
        {
            Token = Env.GetString("DISCORD_TOKEN") ??
                    throw new ArgumentNullException("DISCORD_TOKEN is not configured"),
            Intents = GatewayIntents.Guilds | GatewayIntents.GuildMessages | GatewayIntents.MessageContent,
            LogLevel = LogSeverity.Debug
        });
        services.AddSingleton<ICommandMapper, CommandMapper>();
        services.AddSingleton<MessageHandler>();
        services.AddSingleton<DiscordBotService>();
        services.AddSingleton<LogHandler>();
        services.AddHostedService<DiscordBotService>();

        services.AddDiscordCommandHandlers();
    })
    .Build();


await builder.RunAsync();


