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

    public async Task<bool> MergeAsync(IEnumerable<Store> stores, CancellationToken cancellationToken = default)
    {
        try
        {
            var enumerable = stores.ToList();

            logger.LogInformation("Se están insertando tiendas asociadas a la red Galicia y la categoría {categoryId}",
                enumerable.FirstOrDefault()?.CategoryId);
            await _context.Stores.BulkMergeAsync(enumerable, options =>
            {
                options.IncludeGraph = true;
            }, cancellationToken);

            return true;
        }
        catch (Exception)
        {
            // TODO: Log the exception
            return false;
        }
    }
}