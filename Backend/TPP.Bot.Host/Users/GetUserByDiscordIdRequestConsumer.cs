using Discord.WebSocket;
using MassTransit;
using TPP.Bot.Host.Settings;
using Microsoft.Extensions.Options;
using TPP.MessageBus.Shared.Users.GetDiscordUserByDiscordId;

namespace TPP.Bot.Host.Users;

internal class GetUserByDiscordIdRequestConsumer(DiscordSocketClient discordSocketClient, IOptions<DiscordSettings> discordOptions) 
    : IConsumer<GetDiscordUserByDiscordIdRequest>
{
    private readonly DiscordSettings _discordSettings = discordOptions.Value;

    public async Task Consume(ConsumeContext<GetDiscordUserByDiscordIdRequest> context)
    {
        var guild = discordSocketClient.GetGuild(_discordSettings.ServerId);
        var user = (await guild.GetUsersAsync().ToListAsync()).First().FirstOrDefault(u => u.Id == context.Message.Id);

        await context.RespondAsync(new GetDiscordUserByDiscordIdResponse()
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            AvatarUrl = string.IsNullOrEmpty(user.AvatarId) ? null : string.Format(DiscordConsts.AvatarUrlFormat, user.Id, user.AvatarId),
            Status = DiscordUserStatusMapper.Map(user.Status)
        });
    }
}
