namespace TPP.Api.Startup;

public static class CorsConfiguration
{
    public const string PolicyName = "AllowAll";

    public static void ConfigureCors(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(PolicyName,
                builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
        });
    }
}