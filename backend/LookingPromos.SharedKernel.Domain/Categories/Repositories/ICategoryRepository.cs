using LookingPromos.SharedKernel.Domain.Categories.Entities;
using LookingPromos.SharedKernel.Models;
using MongoDB.Bson;

namespace LookingPromos.SharedKernel.Domain.Categories.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetWithNetworkAssociationAsync(ObjectId categoryId, CancellationToken cancellationToken = default);
}