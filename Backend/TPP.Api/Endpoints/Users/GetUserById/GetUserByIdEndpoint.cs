using MassTransit;
using Microsoft.EntityFrameworkCore;
using TPP.Api.EfCore;
using TPP.MessageBus.Shared.Users.GetDiscordUserByDiscordId;

namespace TPP.Api.Endpoints.Users.GetUserById;

public static class GetUserByIdEndpoint
{
    public static void MapGetUserByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users/{id}/posts", async (IRequestClient<GetDiscordUserByDiscordIdRequest> client, TppDbContext dbContext, Guid id) =>
        {
            var user = await dbContext.Users.Include(u => u.Posts).FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                return Results.NotFound("User not found");
            }

            using var request = client.Create(new GetDiscordUserByDiscordIdRequest(default)
            {
                Id = user.DiscordUserId
            });
            var response = await request.GetResponse<GetDiscordUserByDiscordIdResponse>();

            var discordUser = response.Message;

            var posts = user.Posts.Select(p => new GetUserByIdResponse.Post()
            {
                Text = p.Text,
                IsPositive = p.IsPositive
            }).ToList();

            var count = user.Posts.Count;
            var countPositive = user.Posts.Count(p => p.IsPositive);

            return Results.Ok(new GetUserByIdResponse()
            {
                Id = id,
                AvatarUrl = discordUser.AvatarUrl,
                DisplayName = discordUser.DisplayName,
                Status = discordUser.Status,

                Posts = posts,
                TotalNegativeCount = count - countPositive,
                TotalPositiveCount = countPositive,
            });
        });

    }
}