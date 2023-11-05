using Discord.WebSocket;
using Discord;
using MassTransit;
using TPP.MessageBus.Shared.Users;
using TPP.Bot.Host.Settings;
using Microsoft.Extensions.Options;

namespace TPP.Bot.Host.Users;

internal class GetUserByDiscordIdRequestConsumer : IConsumer<GetDiscordUserByDiscordIdRequest>
{
    private readonly DiscordSocketClient _discordSocketClient;
    private readonly DiscordSettings _discordSettings;

    public GetUserByDiscordIdRequestConsumer(DiscordSocketClient discordSocketClient, IOptions<DiscordSettings> discordOptions)
    {
        _discordSocketClient = discordSocketClient;
        _discordSettings = discordOptions.Value;
    }

    public async Task Consume(ConsumeContext<GetDiscordUserByDiscordIdRequest> context)
    {
        var guild = _discordSocketClient.GetGuild(_discordSettings.ServerId);
        var user = (await guild.GetUsersAsync().ToListAsync()).First().FirstOrDefault(u => u.Id == context.Message.Id);

        var avatarUrlFormat = "https://cdn.discordapp.com/avatars/{0}/{1}.png";

        await context.RespondAsync(new GetDiscordUserByDiscordIdResponse()
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            AvatarUrl = string.Format(avatarUrlFormat, user.Id, user.AvatarId),
            IsOnline = user.Status == UserStatus.Online,
        });
    }
}
