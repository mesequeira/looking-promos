using LookingPromos.Worker.Application.Categories.Consumers;
using LookingPromos.Worker.Application.Networks.Consumers;
using LookingPromos.Worker.Infrastructure.MessageBroker.Options;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LookingPromos.Worker.Infrastructure.MessageBroker.Extensions;

public static class MessageBrokerExtensions
{
    public static void AddMessageBrokerExtensions(this IServiceCollection services)
    {
        services
            .AddOptionsWithValidateOnStart<MessageBrokerOptions>()
            .BindConfiguration(nameof(MessageBrokerOptions))
            .ValidateOnStart();
        
        services.AddMassTransit(configurator =>
        {
            configurator.SetKebabCaseEndpointNameFormatter();
            configurator.AddDelayedMessageScheduler();
            configurator.AddConsumer<GetNetworksEventConsumer>(options =>
            {
                options.ConcurrentMessageLimit = 1;
            });
            configurator.AddConsumer<CategoryCreatedEventConsumer>();

            configurator.UsingRabbitMq(
                (context, cfg) =>
                {
                    var options = context.GetRequiredService<IOptions<MessageBrokerOptions>>().Value;
                    
                    cfg.Host(options.ConnectionString);
                    cfg.UseDelayedMessageScheduler();
                    cfg.ConfigureEndpoints(context);
                }
            );
        });

    }
}