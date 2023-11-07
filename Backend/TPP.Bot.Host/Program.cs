using Discord;
using Discord.Addons.Hosting;
using Discord.WebSocket;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TPP.Bot.Host.Settings;
using TPP.Bot.Host.Users;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder =>
    {
        builder
        .AddJsonFile("appsettings.json")
        .AddJsonFile("appsettings.Development.json", true)
        .AddEnvironmentVariables();
    })
    .ConfigureServices((hostBuilder, services) =>
    {
        services.Configure<DiscordSettings>(hostBuilder.Configuration.GetSection(DiscordSettings.Section));

        Console.WriteLine("XYP " + hostBuilder.Configuration.GetValue<string>("MessageBusHost"));

        services.AddMassTransit(cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();

            cfg.AddConsumer<GetUsersRequestConsumer>();
            cfg.AddConsumer<GetUserByDiscordIdRequestConsumer>();

            cfg.UsingRabbitMq((brc, rbfc) =>
            {
                rbfc.UseDelayedMessageScheduler();
                rbfc.Host(hostBuilder.Configuration.GetValue<string>("MessageBusHost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                rbfc.ConfigureEndpoints(brc);
            });
        });
    })
    .ConfigureDiscordHost((hostBuilder, config) =>
    {
        config.SocketConfig = new DiscordSocketConfig
        {
            AlwaysDownloadUsers = true,
            GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildMembers | GatewayIntents.GuildPresences
        };

        var discordSettings = hostBuilder.Configuration.GetSection(DiscordSettings.Section).Get<DiscordSettings>();

        config.Token = discordSettings!.BotToken;
    })
    .UseCommandService((context, config) =>
    {
        config.DefaultRunMode = Discord.Commands.RunMode.Sync;
    })
    .UseConsoleLifetime();

using var host = builder.Build();

await host.RunAsync();
