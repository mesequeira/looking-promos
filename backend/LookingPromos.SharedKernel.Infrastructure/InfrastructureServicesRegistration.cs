using LookingPromos.SharedKernel.Infrastructure.Galicia.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace LookingPromos.SharedKernel.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static void AddSharedKernelInfrastructureServices(
        this IServiceCollection services
    )
    {
        services.AddHttpContextAccessor();
        services.AddGaliciaConnection();
    }
}