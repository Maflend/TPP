namespace TPP.MessageBus.Shared.Users;
public class GetDiscordUsersRequest
{
}


public class GetDiscordUsersResponse
{
    public List<DiscordUser> Users { get; set; }
}

public class DiscordUser
{
    public ulong Id { get; set; }
    public string DisplayName { get; set; }
    public string AvatarUrl { get; set; }
    public bool IsOnline { get; set; }
}
