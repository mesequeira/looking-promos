using ClaLookingPromos.SharedKernel.Contracts.Galicia.Options;
using LookingPromos.SharedKernel.Domain.Galicia.Services;
using LookingPromos.SharedKernel.Infrastructure.Abstractions.Handlers;
using LookingPromos.SharedKernel.Infrastructure.Galicia.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LookingPromos.SharedKernel.Infrastructure.Galicia.Dependencies;

public static class GaliciaConnectionExtensions
{
    public static void AddGaliciaConnection(this IServiceCollection services)
    {
        services.AddScoped<IGaliciaService, GaliciaService>();
        
        services
            .AddOptionsWithValidateOnStart<GaliciaConnectionOptions>()
            .BindConfiguration($"{nameof(GaliciaConnectionOptions)}")
            .ValidateOnStart();

        services.AddHttpClient(nameof(GaliciaService), (provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<GaliciaConnectionOptions>>().Value;

                client.BaseAddress = options.Url;
                client.DefaultRequestHeaders.Add("User-Agent", nameof(LookingPromos));
            })
            .AddPolicyHandler(ResilienceHandler.CreateRetryPolicy());
    }
}