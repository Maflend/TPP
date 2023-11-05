using Discord;
using Discord.WebSocket;
using MassTransit;
using Microsoft.Extensions.Options;
using TPP.Bot.Host.Settings;
using TPP.MessageBus.Shared.Users;

namespace TPP.Bot.Host.Users;

internal class GetUsersRequestConsumer : IConsumer<GetDiscordUsersRequest>
{
    private readonly DiscordSocketClient _discordSocketClient;
    private readonly DiscordSettings _discordSettings;

    public GetUsersRequestConsumer(DiscordSocketClient discordSocketClient, IOptions<DiscordSettings> discordOptions)
    {
        _discordSocketClient = discordSocketClient;
        _discordSettings = discordOptions.Value;
    }

    public async Task Consume(ConsumeContext<GetDiscordUsersRequest> context)
    {
        var guild = _discordSocketClient.GetGuild(_discordSettings.ServerId);
        var guildUsers = (await guild.GetUsersAsync().ToListAsync()).First();

        var avatarUrlFormat = "https://cdn.discordapp.com/avatars/{0}/{1}.png";

        var users = guildUsers.Where(x => !x.IsBot).Select(x => new DiscordUser
        {
            Id = x.Id,
            DisplayName = x.DisplayName,
            AvatarUrl = string.Format(avatarUrlFormat, x.Id, x.AvatarId),
            IsOnline = x.Status == UserStatus.Online,
        }).ToList();

        await context.RespondAsync(new GetDiscordUsersResponse()
        {
            Users = users
        });
    }
}
