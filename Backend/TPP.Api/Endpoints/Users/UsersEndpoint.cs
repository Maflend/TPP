using TPP.Api.Endpoints.Users.GetUserById;
using TPP.Api.Endpoints.Users.GetUsers;

namespace TPP.Api.Endpoints.Users;

public static class UsersEndpoint
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGetUsersEndpoint();
        app.MapGetUserByIdEndpoint();
    }
}
