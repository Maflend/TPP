using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TPP.Bot.Host.Settings;
using TPP.Bot.Host.Startup;

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
        services.ConfigureMassTransit(hostBuilder);
    })
    .ConfigureDiscordHost()
    .UseConsoleLifetime();

using var host = builder.Build();

await host.RunAsync();
