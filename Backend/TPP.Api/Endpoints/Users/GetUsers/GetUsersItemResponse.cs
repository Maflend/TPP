using TPP.MessageBus.Shared.Common;

namespace TPP.Api.Endpoints.Users.GetUsers;

public class GetUsersItemResponse
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
    public string AvatarUrl { get; set; }
    public DiscordUserStatus Status { get; set; }

    public int TotalPositiveCount { get; set; }
    public int TotalNegativeCount { get; set; }
}
