using System.Data;
using LookingPromos.SharedKernel.Models;
using LookingPromos.SharedKernel.Persistence.Abstractions.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace LookingPromos.SharedKernel.Persistence.Abstractions.Repositories;

/// <summary>
/// The class that implements the unit of work pattern.
/// </summary>
internal sealed class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IDbTransaction> BeginTransactionAsync(
        CancellationToken cancellationToken = default
    )
    {
        return (await context.Database.BeginTransactionAsync(cancellationToken)).GetDbTransaction();
    }
}
