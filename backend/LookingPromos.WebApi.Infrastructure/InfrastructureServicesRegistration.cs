using LookingPromos.SharedKernel.Infrastructure;
using LookingPromos.WebApi.Infrastructure.MessageBroker.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace LookingPromos.WebApi.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static void AddInfrastructureServices(
        this IServiceCollection services
    )
    {
        services.AddSharedKernelInfrastructureServices();
        services.AddMessageBrokerConfiguration();
    }
}