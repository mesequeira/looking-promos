using LookingPromos.SharedKernel.Domain.Categories.Repositories;
using LookingPromos.SharedKernel.Domain.Networks.Repositories;
using LookingPromos.SharedKernel.Domain.Stores.Repositories;
using LookingPromos.SharedKernel.Models;
using LookingPromos.SharedKernel.Persistence.Abstractions.Dependencies;
using LookingPromos.SharedKernel.Persistence.Abstractions.Repositories;
using LookingPromos.SharedKernel.Persistence.Categories.Repositories;
using LookingPromos.SharedKernel.Persistence.Networks.Repositories;
using LookingPromos.SharedKernel.Persistence.Stores.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LookingPromos.SharedKernel.Persistence;

public static class PersistenceServicesRegistration
{
    public static void AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<INetworkRepository, NetworkRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IStoreRepository, StoreRepository>();
        
        
        services.IntegrateDatabaseContext();
    }
}
