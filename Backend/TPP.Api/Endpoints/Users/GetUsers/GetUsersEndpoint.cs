using MassTransit;
using Microsoft.EntityFrameworkCore;
using TPP.Api.Domain;
using TPP.Api.EfCore;
using TPP.MessageBus.Shared.Users.GetDiscordUsers;

namespace TPP.Api.Endpoints.Users.GetUsers;

public static class UsersEndpoint
{
    public static void MapGetUsersEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users", async (
            IRequestClient<GetDiscordUsersRequest> client,
            TppDbContext dbContext,
            [AsParameters] GetUsersRequest getUsersRequest) =>
        {
            using var request = client.Create(new GetDiscordUsersRequest());
            var response = await request.GetResponse<GetDiscordUsersResponse>();

            var discordIds = response.Message.Users.Select(u => u.Id).ToList();

            var users = await dbContext.Users.ToListAsync();
            var discordIdsFromDb = users.Select(u => u.DiscordUserId).ToList();

            discordIds = discordIds.Where(d => !discordIdsFromDb.Contains(d)).ToList();

            await dbContext.Users.AddRangeAsync(discordIds.Select(discordId => new User
            {
                DiscordUserId = discordId
            }));

            await dbContext.SaveChangesAsync();

            var usersResponse = (await dbContext.Users.Select(u => new
            {
                u.Id,
                u.DiscordUserId,
                TotalNegativeCount = u.Posts.Count - u.Posts.Count(p => p.IsPositive),
                TotalPositiveCount = u.Posts.Count(p => p.IsPositive)
            }).ToListAsync()).Select(u =>
            {
                var discordUser = response.Message.Users.FirstOrDefault(du => du.Id == u.DiscordUserId);

                if (discordUser is null)
                {
                    return null;
                }

                return new GetUsersItemResponse()
                {
                    Id = u.Id,
                    AvatarUrl = discordUser!.AvatarUrl,
                    DisplayName = discordUser.DisplayName,
                    Status = discordUser.Status,
                    TotalNegativeCount = u.TotalNegativeCount,
                    TotalPositiveCount = u.TotalPositiveCount
                };
            }).Where(x => x is not null);

            switch (getUsersRequest.OrderType)
            {
                case OrderTypeEnum.ByOnline:
                    usersResponse = usersResponse.OrderByDescending(u => u.Status == MessageBus.Shared.Common.DiscordUserStatus.Online);
                    break;
                case OrderTypeEnum.ByPositive:
                    usersResponse = usersResponse.OrderByDescending(u => u.TotalPositiveCount);
                    break;
                case OrderTypeEnum.ByNegative:
                    usersResponse = usersResponse.OrderByDescending(u => u.TotalNegativeCount);
                    break;
            }

            return usersResponse;
        });
    }
}
