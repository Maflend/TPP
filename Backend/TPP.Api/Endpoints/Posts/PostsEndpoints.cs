using TPP.Api.Endpoints.Posts.CreatePost;

namespace TPP.Api.Endpoints.Posts;

public static class PostsEndpoints
{
    public static void MapPostEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreatePostEndpoint();
    }
}
