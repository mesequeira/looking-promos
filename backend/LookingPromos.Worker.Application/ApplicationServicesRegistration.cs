using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace LookingPromos.Worker.Application;

/// <summary>
/// Registers the application services for the worker.
/// </summary>
public static class ApplicationServicesRegistration
{
    /// <summary>
    /// Extension method to add the application services for the worker.
    /// </summary>
    /// <param name="services">An instance of <see cref="IServiceCollection"/>.</param>
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
            cfg.NotificationPublisher = new TaskWhenAllPublisher();
        });
    }
}