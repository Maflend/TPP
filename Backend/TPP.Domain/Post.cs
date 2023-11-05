namespace TPP.Domain;
public class Post
{
	public Guid Id { get; set; }
	public string Text { get; set; } = string.Empty;
    public bool IsPositive { get; set; }

    public Guid UserId { get; set; }
	public User User { get; set; }
}
