namespace TPP.Api.Domain;

public class User
{
	public Guid Id { get; set; }
	public ulong DiscordUserId { get; set; }

	public ICollection<Post> Posts { get; set; }
}
