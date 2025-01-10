using System.Net;
using ClaLookingPromos.SharedKernel.Contracts.Categories.Interfaces;
using ClaLookingPromos.SharedKernel.Contracts.Stores;
using LookingPromos.SharedKernel.Domain.Categories.Entities;
using LookingPromos.SharedKernel.Domain.Networks.Entities;
using LookingPromos.SharedKernel.Domain.ProviderErrors.Constants;
using LookingPromos.SharedKernel.Domain.ProviderErrors.Entities;
using LookingPromos.SharedKernel.Models;
using MongoDB.Bson;

namespace LookingPromos.SharedKernel.Domain.Stores.Entities;

public class Store : Entity
{
    public string Name { get; set; } = default!;
    public long ExternalStoreId { get; set; }
    public ObjectId CategoryId { get; set; } = default!;
    public virtual Category Category { get; set; } = default!;
    
    public static Result<(IEnumerable<Store>?, ProviderError?)> Create(
        Result<IEnumerable<IStoreResponse>> response,
        ObjectId categoryId
    )
    {
        // If the status code is higher than 300, we determine that the response got an error.
        if (response is { IsFailure: true, HttpStatusCode: > HttpStatusCode.MultipleChoices })
        {
            var providerError = ProviderError.Create(
                response.Error,
                ProviderEndpoints.GetCaterogiesGalicia
            );

            if (providerError is { IsSuccess: true })
            {
                return Result.Failure<(IEnumerable<Store>?, ProviderError?)>(
                    (default, providerError.Value),
                    providerError.Error,
                    providerError.HttpStatusCode
                );
            }
        }

        IEnumerable<Store> stores = response.Value.Select(category => new Store
        {
            Name = category.Name,
            CategoryId = categoryId,
            ExternalStoreId = category.ExternalStoreId
        }).ToList();

        return Result.Success<(IEnumerable<Store>?, ProviderError?)>((stores, default));
    }
}