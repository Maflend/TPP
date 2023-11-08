using Discord.WebSocket;
using MassTransit;
using Microsoft.Extensions.Options;
using TPP.Bot.Host.Settings;
using TPP.MessageBus.Shared.Users.GetDiscordUsers;

namespace TPP.Bot.Host.Users;

internal class GetUsersRequestConsumer(DiscordSocketClient discordSocketClient, IOptions<DiscordSettings> discordOptions) 
    : IConsumer<GetDiscordUsersRequest>
{
    private readonly DiscordSettings _discordSettings = discordOptions.Value;

    public async Task Consume(ConsumeContext<GetDiscordUsersRequest> context)
    {
        var guild = discordSocketClient.GetGuild(_discordSettings.ServerId);

        var guildUsers = (await guild.GetUsersAsync().ToListAsync()).First();

        var users = guildUsers
            .Where(user => !user.IsBot)
            .Select(user => new DiscordUser
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                AvatarUrl = string.IsNullOrEmpty(user.AvatarId) ? null : string.Format(DiscordConsts.AvatarUrlFormat, user.Id, user.AvatarId),
                Status = DiscordUserStatusMapper.Map(user.Status)
            }).ToList();

        await context.RespondAsync(new GetDiscordUsersResponse()
        {
            Users = users
        });
    }
}
