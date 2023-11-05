namespace TPP.Api.Models;

public class CreatePostRequest
{
    public Guid UserId { get; set; }
    public string Text { get; set; }
    public bool IsPositive { get; set; }
}

public class GetPostsResponse
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
    public string AvatarUrl { get; set; }
    public bool IsOnline { get; set; }

    public int TotalPositiveCount { get; set; }
    public int TotalNegativeCount { get; set; }

    public List<GetPostsItemResponse> Posts { get; set; }
}

public class GetPostsItemResponse
{
    public string Text { get; set; }
    public bool IsPositive { get; set; }
}
