using TPP.MessageBus.Shared.Common;

namespace TPP.MessageBus.Shared.Users.GetDiscordUserByDiscordId;

public class GetDiscordUserByDiscordIdResponse
{
    public ulong Id { get; set; }
    public string DisplayName { get; set; } = null!;
    public string? AvatarUrl { get; set; }
    public DiscordUserStatus Status { get; set; }
}
