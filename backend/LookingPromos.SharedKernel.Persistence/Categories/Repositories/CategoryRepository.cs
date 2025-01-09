using LookingPromos.SharedKernel.Domain.Categories.Entities;
using LookingPromos.SharedKernel.Domain.Categories.Repositories;
using LookingPromos.SharedKernel.Persistence.Abstractions.Contexts;
using LookingPromos.SharedKernel.Persistence.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LookingPromos.SharedKernel.Persistence.Categories.Repositories;

internal sealed class CategoryRepository(ApplicationDbContext context
) : Repository<Category>(context), ICategoryRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> MergeAsync(IEnumerable<Category> categories, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.Categories.BulkMergeAsync(categories,
                options =>
                {
                    options.IncludeGraph = true;
                },
                cancellationToken);

            return true;
        }
        catch (Exception)
        {
            // TODO: Log the exception
            return false;
        }
    }

    public async Task<Category?> GetWithNetworkAssociationAsync(long categoryId, CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .Include(category => category.Network)
            .FirstOrDefaultAsync(category => category.Id == categoryId, cancellationToken); 
    }
}