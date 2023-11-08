using Discord;
using TPP.MessageBus.Shared.Common;

namespace TPP.Bot.Host.Users;
internal class DiscordUserStatusMapper
{
    public static DiscordUserStatus Map(UserStatus userStatus) => userStatus switch
    {
        UserStatus.Online => DiscordUserStatus.Online,
        UserStatus.Offline => DiscordUserStatus.Offline,
        UserStatus.Idle => DiscordUserStatus.Idle,
        UserStatus.AFK => DiscordUserStatus.AFK,
        UserStatus.DoNotDisturb => DiscordUserStatus.DoNotDisturb,
        UserStatus.Invisible => DiscordUserStatus.Invisible,
        _ => throw new ArgumentException("Не предусмотрен маппинг статуса юзера в Discord")
    };
}
