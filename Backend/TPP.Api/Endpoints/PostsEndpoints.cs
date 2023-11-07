using MassTransit;
using Microsoft.EntityFrameworkCore;
using TPP.Api.Domain;
using TPP.Api.EfCore;
using TPP.Api.Models;
using TPP.MessageBus.Shared.Users;

namespace TPP.Api.Endpoints;

public static class PostsEndpoints
{
    public static void MapPostEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users/{id}/posts", async (IRequestClient<GetDiscordUserByDiscordIdRequest> client, TppDbContext dbContext, Guid id) =>
        {
            var user = await dbContext.Users.Include(u => u.Posts).FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                return Results.NotFound("User not found");
            }

            using var request = client.Create(new GetDiscordUserByDiscordIdRequest()
            {
                Id = user.DiscordUserId
            });
            var response = await request.GetResponse<GetDiscordUserByDiscordIdResponse>();

            var discordUser = response.Message;

            var posts = user.Posts.Select(p => new GetPostsItemResponse()
            {
                Text = p.Text,
                IsPositive = p.IsPositive
            }).ToList();

            var count = user.Posts.Count;
            var countPositive = user.Posts.Count(p => p.IsPositive);

            return Results.Ok(new GetPostsResponse()
            {
                Id = id,
                AvatarUrl = discordUser.AvatarUrl,
                DisplayName = discordUser.DisplayName,
                IsOnline = discordUser.IsOnline,

                Posts = posts,
                TotalNegativeCount = count - countPositive,
                TotalPositiveCount = countPositive,
            });
        });

        app.MapPost("/api/posts", async (TppDbContext dbContext, CreatePostRequest request) =>
        {
            var userExist = await dbContext.Users.AnyAsync(u => u.Id == request.UserId);

            if (!userExist)
            {
                return Results.NotFound("User not found");
            }

            var post = new Post()
            {
                Text = request.Text,
                IsPositive = request.IsPositive,
                UserId = request.UserId,
            };

            dbContext.Posts.Add(post);

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}
