using MassTransit;
using Microsoft.EntityFrameworkCore;
using TPP.Api.EfCore;
using TPP.Api.Endpoints;
using TPP.Api.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddNpgsql<TppDbContext>(builder.Configuration.GetConnectionString("Postgres"));

builder.Services.ConfigureMassTransit(builder);
builder.Services.ConfigureCors(builder);

var app = builder.Build();

await ApplyMigrations();

app.UseCors(CorsConfiguration.PolicyName);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapUserEndpoints();
app.MapPostEndpoints();

app.Run();

async Task ApplyMigrations()
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<TppDbContext>();
    var autoMigrate = app.Configuration.GetValue<bool>("AutoMigrate");

    var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

    if (autoMigrate && pendingMigrations.Any())
    {
        await dbContext.Database.EnsureCreatedAsync();
    }
}