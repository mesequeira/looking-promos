using LookingPromos.SharedKernel.Domain.Categories.Entities;
using LookingPromos.SharedKernel.Models;

namespace LookingPromos.SharedKernel.Domain.Categories.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<bool> MergeAsync(IEnumerable<Category> categories, CancellationToken cancellationToken = default);
    
    Task<Category?> GetWithNetworkAssociationAsync(long categoryId, CancellationToken cancellationToken = default);
}