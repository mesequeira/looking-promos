using LookingPromos.SharedKernel.Domain.Networks.Entities;
using LookingPromos.SharedKernel.Domain.Networks.Enums;
using LookingPromos.SharedKernel.Domain.Networks.Factories;
using LookingPromos.SharedKernel.Domain.Networks.Strategies;
using LookingPromos.SharedKernel.Infrastructure.Galicia.Strategies;

namespace LookingPromos.SharedKernel.Infrastructure.Networks.Factories;

public sealed class NetworkFactory(IEnumerable<INetworkStrategy> strategies)
    : INetworkFactory
{
    public async Task<bool> CreateStrategyAsync(Network network, CancellationToken cancellationToken = default)
    {
        var networkId = network.Id.ToString();

        var strategy = strategies.FirstOrDefault(strategy =>
        {
            return strategy switch
            {
                GaliciaStrategy => networkId == NetworkVariants.Galicia,
                _ => false
            };
        });

        ArgumentNullException.ThrowIfNull(strategy);
        
        return await strategy.GetNetworkPromotionsAsync(cancellationToken);
    }
}