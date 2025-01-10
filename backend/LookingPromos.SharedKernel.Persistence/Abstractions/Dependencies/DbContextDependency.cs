using LookingPromos.SharedKernel.Persistence.Abstractions.Contexts;
using LookingPromos.SharedKernel.Persistence.Abstractions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LookingPromos.SharedKernel.Persistence.Abstractions.Dependencies;

/// <summary>
/// The class that contains the extension method to add the database context with their interceptors to the service collection.
/// </summary>
public static class DbContextDependency
{
    public static void IntegrateDatabaseContext(
        this IServiceCollection services
    )
    {
        services
            .AddOptionsWithValidateOnStart<MongoDbConnectionOptions>()
            .BindConfiguration(nameof(MongoDbConnectionOptions))
            .ValidateOnStart();
        
        services.AddDbContext<ApplicationDbContext>(
            (provider, context) =>
            {
                var options = provider.GetRequiredService<IOptions<MongoDbConnectionOptions>>().Value;
                
                context.UseMongoDB(
                    options.ConnectionString,
                    options.DatabaseName
                );
            }
        );
    }
}