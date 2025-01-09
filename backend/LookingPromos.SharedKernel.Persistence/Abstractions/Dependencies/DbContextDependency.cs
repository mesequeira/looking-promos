using LookingPromos.SharedKernel.Persistence.Abstractions.Contexts;
using LookingPromos.SharedKernel.Persistence.Abstractions.Contexts.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LookingPromos.SharedKernel.Persistence.Abstractions.Dependencies;

/// <summary>
/// The class that contains the extension method to add the database context with their interceptors to the service collection.
/// </summary>
public static class DbContextDependency
{
    public static void AddDbContext(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        services.AddSingleton<DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>(
            (sp, options) =>
            {
                var auditableInterceptor = sp.GetService<UpdateAuditableEntitiesInterceptor>()!;
                var dispatchDomainEventsInterceptor =
                    sp.GetService<DispatchDomainEventsInterceptor>()!;

                options
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                    .AddInterceptors(auditableInterceptor, dispatchDomainEventsInterceptor);
            }
        );
    }
}
