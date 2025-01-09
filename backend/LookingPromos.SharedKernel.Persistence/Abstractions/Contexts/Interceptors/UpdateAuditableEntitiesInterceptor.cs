using LookingPromos.SharedKernel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LookingPromos.SharedKernel.Persistence.Abstractions.Contexts.Interceptors;

/// <summary>
/// Everytime a save operation is performed, this interceptor will update the CreatedAt and ModifiedAt properties of the entities.
/// </summary>
public sealed class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        var dbContext = eventData.Context;

        if (dbContext is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        var entries = dbContext
            .ChangeTracker.Entries<Entity>()
            .Where(m => m.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            var utcNow = DateTime.UtcNow;

            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(a => a.CreatedAt).CurrentValue = utcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(a => a.ModifiedAt).CurrentValue = DateTime.UtcNow;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
