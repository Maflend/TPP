using Discord;
using Discord.Addons.Hosting;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using TPP.Bot.Host.Settings;

namespace TPP.Bot.Host.Startup;
internal static class DiscordHostConfiguration
{
    public static IHostBuilder ConfigureDiscordHost(this IHostBuilder builder)
    {
        builder.ConfigureDiscordHost((hostBuilder, config) =>
        {
            config.SocketConfig = new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildMembers | GatewayIntents.GuildPresences
            };

            var discordSettings = hostBuilder.Configuration.GetSection(DiscordSettings.Section).Get<DiscordSettings>();

            config.Token = discordSettings!.BotToken;
        });

        builder.UseCommandService((context, config) =>
        {
            config.DefaultRunMode = Discord.Commands.RunMode.Sync;
        });

        return builder;
    }
}
