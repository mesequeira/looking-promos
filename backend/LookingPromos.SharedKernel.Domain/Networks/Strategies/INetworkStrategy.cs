using LookingPromos.SharedKernel.Domain.Categories.Entities;

namespace LookingPromos.SharedKernel.Domain.Networks.Strategies;

public interface INetworkStrategy
{
    Task<bool> GetNetworkPromotionsAsync(CancellationToken cancellationToken = default);
    
    Task<bool> GetStoresAssociatedWithCategoryAsync(Category category, CancellationToken cancellationToken = default);
}