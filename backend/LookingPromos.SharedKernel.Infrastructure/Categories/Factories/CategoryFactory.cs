using LookingPromos.SharedKernel.Domain.Categories.Entities;
using LookingPromos.SharedKernel.Domain.Categories.Factories;
using LookingPromos.SharedKernel.Domain.Networks.Enums;
using LookingPromos.SharedKernel.Domain.Networks.Strategies;
using LookingPromos.SharedKernel.Infrastructure.Galicia.Strategies;

namespace LookingPromos.SharedKernel.Infrastructure.Categories.Factories;

public class CategoryFactory(
    IEnumerable<INetworkStrategy> strategies    
) : ICategoryFactory
{
    
    public async Task<bool> CreateStrategyAsync(Category category, CancellationToken cancellationToken = default)
    {
        var networkId = category.NetworkId.ToString();

        var strategy = strategies.FirstOrDefault(strategy =>
        {
            return strategy switch
            {
                GaliciaStrategy => networkId == NetworkVariants.Galicia,
                _ => false
            };
        });

        ArgumentNullException.ThrowIfNull(strategy);
        
        return await strategy.GetStoresAssociatedWithCategoryAsync(category, cancellationToken);
    }
}