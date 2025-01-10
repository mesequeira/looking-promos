using LookingPromos.SharedKernel.Domain.Categories.Entities;
using LookingPromos.SharedKernel.Domain.Categories.Repositories;
using LookingPromos.SharedKernel.Persistence.Abstractions.Contexts;
using LookingPromos.SharedKernel.Persistence.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace LookingPromos.SharedKernel.Persistence.Categories.Repositories;

internal sealed class CategoryRepository(
    ApplicationDbContext context
) : Repository<Category>(context), ICategoryRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Category?> GetWithNetworkAssociationAsync(
        ObjectId categoryId,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(category => category.Id == categoryId, cancellationToken);

            if (category is null)
            {
                return default;
            }

            category.Network = await _context.Networks
                .FirstOrDefaultAsync(network => network.Id == category.NetworkId, cancellationToken);

            return category;
        }
        catch (Exception ex)
        {
            return default;
        }
    }
}