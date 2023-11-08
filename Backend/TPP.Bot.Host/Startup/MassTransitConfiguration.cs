using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TPP.Bot.Host.Users;

namespace TPP.Bot.Host.Startup;

internal static class MassTransitConfiguration
{
    public static void ConfigureMassTransit(this IServiceCollection services, HostBuilderContext builder)
    {
        services.AddMassTransit(cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();

            cfg.AddConsumer<GetUsersRequestConsumer>();
            cfg.AddConsumer<GetUserByDiscordIdRequestConsumer>();

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
