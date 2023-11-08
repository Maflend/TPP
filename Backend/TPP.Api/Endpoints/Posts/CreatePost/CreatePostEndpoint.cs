using Microsoft.EntityFrameworkCore;
using TPP.Api.Domain;
using TPP.Api.EfCore;

namespace TPP.Api.Endpoints.Posts.CreatePost;

public static class PostsEndpoints
{
    public static void MapCreatePostEndpoint(this IEndpointRouteBuilder app)
    {
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
