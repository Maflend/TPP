using MassTransit;

namespace TPP.Api.Startup;

public static class MassTransitConfiguration
{
    public static void ConfigureMassTransit(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddMassTransit(cfg =>
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
    }
}
