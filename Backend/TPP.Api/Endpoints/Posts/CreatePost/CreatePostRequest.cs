namespace TPP.Api.Endpoints.Posts.CreatePost;

public class CreatePostRequest
{
    public Guid UserId { get; set; }
    public string Text { get; set; }
    public bool IsPositive { get; set; }
}