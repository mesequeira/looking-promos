using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LookingPromos.SharedKernel.Models;
using LookingPromos.SharedKernel.Persistence.Abstractions.Contexts;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace LookingPromos.SharedKernel.Persistence.Abstractions.Repositories;

internal class Repository<TEntity>(ApplicationDbContext context) : IRepository<TEntity>
    where TEntity : Entity
{
    public async Task<PaginatedResult<TDto>> GetAsync<TDto>(
        int pageIndex,
        int pageSize,
        IConfigurationProvider configurationProvider,
        CancellationToken cancellationToken = default,
        Expression<Func<TEntity, bool>>? predicate = null
    )
    {
        var query = context.Set<TEntity>().Where(predicate ?? (_ => true));

        var totalItems = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ProjectTo<TDto>(configurationProvider)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<TDto>(items, totalItems, pageIndex, pageSize);
    }

    public Task<List<TEntity>> GetAsync(CancellationToken cancellationToken = default)
    {
        return context.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(ObjectId id, CancellationToken cancellationToken = default)
    {
        return await context
            .Set<TEntity>()
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }
    
    public async Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        context.Set<TEntity>().Update(entity);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        context.Set<TEntity>().Remove(entity);
        await Task.CompletedTask;
    }

    public Task<TValue?> GetByIdProjectionAsync<TValue>(
        ObjectId id,
        IConfigurationProvider configurationProvider,
        CancellationToken cancellationToken = default
    )
    {
        return context
            .Set<TEntity>()
            .Where(e => e.Id == id)
            .ProjectTo<TValue>(configurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
