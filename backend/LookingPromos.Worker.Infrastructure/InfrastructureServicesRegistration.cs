using LookingPromos.SharedKernel.Domain.Categories.Factories;
using LookingPromos.SharedKernel.Domain.Galicia.Services;
using LookingPromos.SharedKernel.Domain.Networks.Factories;
using LookingPromos.SharedKernel.Domain.Networks.Strategies;
using LookingPromos.SharedKernel.Infrastructure;
using LookingPromos.SharedKernel.Infrastructure.Categories.Factories;
using LookingPromos.SharedKernel.Infrastructure.Galicia.Dependencies;
using LookingPromos.SharedKernel.Infrastructure.Galicia.Services;
using LookingPromos.SharedKernel.Infrastructure.Galicia.Strategies;
using LookingPromos.SharedKernel.Infrastructure.Networks.Factories;
using LookingPromos.Worker.Infrastructure.MessageBroker.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace LookingPromos.Worker.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static void AddInfrastructureServices(
        this IServiceCollection services
    )
    {
        // Inject the necessary configuration for all the infrastructure projects to connect with Tapi.
        services.AddSharedKernelInfrastructureServices();
        
        // Inject the necessary configuration to connect with the message broker.
        services.AddMessageBrokerExtensions();
        
        services.AddScoped<INetworkStrategy, GaliciaStrategy>();
        services.AddScoped<INetworkFactory>(provider => new NetworkFactory(
            provider.GetServices<INetworkStrategy>()
        ));
        services.AddScoped<ICategoryFactory>(provider => new CategoryFactory(
            provider.GetServices<INetworkStrategy>()
        ));
    }
}