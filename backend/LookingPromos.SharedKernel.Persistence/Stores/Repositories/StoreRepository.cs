using LookingPromos.SharedKernel.Domain.Stores.Entities;
using LookingPromos.SharedKernel.Domain.Stores.Repositories;
using LookingPromos.SharedKernel.Persistence.Abstractions.Contexts;
using LookingPromos.SharedKernel.Persistence.Abstractions.Repositories;
using Microsoft.Extensions.Logging;

namespace LookingPromos.SharedKernel.Persistence.Stores.Repositories;

internal sealed class StoreRepository(
    ApplicationDbContext context,
    ILogger<StoreRepository> logger
) : Repository<Store>(context), IStoreRepository
{
    private readonly ApplicationDbContext _context = context;

    
}