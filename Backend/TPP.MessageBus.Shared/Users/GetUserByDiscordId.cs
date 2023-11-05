namespace TPP.MessageBus.Shared.Users;

public class GetDiscordUserByDiscordIdRequest
{
    public ulong Id { get; set; }
}


public class GetDiscordUserByDiscordIdResponse
{
    public ulong Id { get; set; }
    public string DisplayName { get; set; }
    public string AvatarUrl { get; set; }
    public bool IsOnline { get; set; }
}
