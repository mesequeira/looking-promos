using LookingPromos.SharedKernel.Domain.Networks.Entities;
using LookingPromos.SharedKernel.Domain.Networks.Repositories;
using LookingPromos.SharedKernel.Persistence.Abstractions.Contexts;
using LookingPromos.SharedKernel.Persistence.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LookingPromos.SharedKernel.Persistence.Networks.Repositories;

internal sealed class NetworkRepository(
    ApplicationDbContext context
) : Repository<Network>(context), INetworkRepository
{
    public async Task<List<Network>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Networks.ToListAsync(cancellationToken);
    }
}