using LookingPromos.WebApi.Infrastructure.MessageBroker.Options;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LookingPromos.WebApi.Infrastructure.MessageBroker.Extensions;

internal static class MessageBrokerExtensions
{
    public static void AddMessageBrokerConfiguration(
        this IServiceCollection services
    )
    {
        services
            .AddOptionsWithValidateOnStart<MessageBrokerOptions>()
            .BindConfiguration(nameof(MessageBrokerOptions))
            .ValidateOnStart();

        services.AddMassTransit(configurator =>
        {
            configurator.SetKebabCaseEndpointNameFormatter();
            
            configurator.UsingRabbitMq((context, cfg) =>
            {
                var options = context.GetRequiredService<IOptions<MessageBrokerOptions>>().Value;

                cfg.Host(options.ConnectionString, h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });
            });
        });
    }
}