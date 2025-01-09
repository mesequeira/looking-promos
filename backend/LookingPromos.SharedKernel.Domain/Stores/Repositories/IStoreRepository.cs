using LookingPromos.SharedKernel.Domain.Stores.Entities;
using LookingPromos.SharedKernel.Models;

namespace LookingPromos.SharedKernel.Domain.Stores.Repositories;

public interface IStoreRepository : IRepository<Store>
{
    Task<bool> MergeAsync(IEnumerable<Store> stores, CancellationToken cancellationToken = default);
}