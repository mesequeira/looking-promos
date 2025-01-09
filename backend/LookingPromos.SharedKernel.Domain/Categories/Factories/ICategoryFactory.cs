using LookingPromos.SharedKernel.Domain.Categories.Entities;

namespace LookingPromos.SharedKernel.Domain.Categories.Factories;

public interface ICategoryFactory
{
    Task<bool> CreateStrategyAsync(Category category, CancellationToken cancellationToken = default);
}