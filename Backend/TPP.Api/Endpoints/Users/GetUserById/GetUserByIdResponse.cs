using TPP.MessageBus.Shared.Common;

namespace TPP.Api.Endpoints.Users.GetUserById;

public class GetUserByIdResponse
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; } = null!;
    public string? AvatarUrl { get; set; }
    public DiscordUserStatus Status { get; set; }

    public int TotalPositiveCount { get; set; }
    public int TotalNegativeCount { get; set; }

    public List<Post> Posts { get; set; } = [];

    public class Post
    {
        public string Text { get; set; }
        public bool IsPositive { get; set; }
    }
}
