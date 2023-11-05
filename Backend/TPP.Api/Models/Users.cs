namespace TPP.Api.Models;

public class GetUsersItemResponse
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
    public string AvatarUrl { get; set; }
    public bool IsOnline { get; set; }

    public int TotalPositiveCount { get; set; }
    public int TotalNegativeCount { get; set; }
}
