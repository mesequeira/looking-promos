using LookingPromos.SharedKernel.Domain.Networks.Entities;
using LookingPromos.SharedKernel.Models;

namespace LookingPromos.SharedKernel.Domain.Networks.Repositories;

public interface INetworkRepository : IRepository<Network>
{
    Task<List<Network>> GetAllAsync(CancellationToken cancellationToken);
}