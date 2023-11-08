using TPP.MessageBus.Shared.Common;

namespace TPP.MessageBus.Shared.Users.GetDiscordUsers;

public class GetDiscordUsersResponse
{
    public List<DiscordUser> Users { get; set; } = [];
}

public class DiscordUser
{
    public ulong Id { get; set; }
    public string DisplayName { get; set; } = null!;
    public string? AvatarUrl { get; set; }
    public DiscordUserStatus Status { get; set; }
}
