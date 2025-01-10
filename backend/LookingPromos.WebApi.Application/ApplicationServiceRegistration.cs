using FluentValidation;
using LookingPromos.WebApi.Application.Abstractions.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace LookingPromos.WebApi.Application;

/// <summary>
/// The class that contains the extension method to register application services.
/// </summary>
public static class ApplicationServiceRegistration
{
    /// <summary>
    /// Injects the application services into the service collection.
    /// </summary>
    /// <param name="services">The current instance of the <see cref="IServiceCollection"/> interface.</param>
    /// <returns>The modified instance of the <see cref="IServiceCollection"/> interface.</returns>
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(ApplicationAssemblyReference.Assembly);
            
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            // config.AddOpenBehavior(typeof(TransactionalPipelineBehavior<,>));
        });

        services.AddAutoMapper(ApplicationAssemblyReference.Assembly);

        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>(
            includeInternalTypes: true
        );
    }
}
