using MassTransit;
using MassTransit.Initializers;
using Microsoft.EntityFrameworkCore;
using TPP.Api.Domain;
using TPP.Api.EfCore;
using TPP.Api.Models;
using TPP.MessageBus.Shared.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddNpgsql<TppDbContext>(builder.Configuration.GetConnectionString("Postgres"));

builder.Services.AddMassTransit(cfg =>
{
    cfg.SetKebabCaseEndpointNameFormatter();

    cfg.UsingRabbitMq((brc, rbfc) =>
    {
        rbfc.UseDelayedMessageScheduler();
        rbfc.Host(builder.Configuration.GetValue<string>("MessageBusHost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        rbfc.ConfigureEndpoints(brc);
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

var dbContext = app.Services.GetRequiredService<TppDbContext>();
var autoMigrate = app.Configuration.GetValue<bool>("AutoMigrate");

if (autoMigrate && (await dbContext.Database.GetPendingMigrationsAsync()).Any())
{
    await dbContext.Database.EnsureCreatedAsync();
}


#region Users

app.MapGet("/api/users", async (IRequestClient<GetDiscordUsersRequest> client, TppDbContext dbContext) =>
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

        return new GetUsersItemResponse()
        {
            Id = u.Id,
            AvatarUrl = discordUser!.AvatarUrl,
            DisplayName = discordUser.DisplayName,
            IsOnline = discordUser.IsOnline,
            TotalNegativeCount = u.TotalNegativeCount,
            TotalPositiveCount = u.TotalPositiveCount
        };
    });

    return usersResponse;
});

#endregion

#region Posts

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

#endregion

app.Run();
