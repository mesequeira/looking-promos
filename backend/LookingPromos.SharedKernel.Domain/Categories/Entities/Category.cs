using System.Net;
using ClaLookingPromos.SharedKernel.Contracts.Categories.Interfaces;
using ClaLookingPromos.SharedKernel.Contracts.Galicia.Responses;
using LookingPromos.SharedKernel.Domain.Networks.Entities;
using LookingPromos.SharedKernel.Domain.ProviderErrors.Constants;
using LookingPromos.SharedKernel.Domain.ProviderErrors.Entities;
using LookingPromos.SharedKernel.Domain.Stores.Entities;
using LookingPromos.SharedKernel.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LookingPromos.SharedKernel.Domain.Categories.Entities;

public class Category : Entity
{
    public string? Name { get; set; }
    
    public long? ProviderCategoryId { get; set; }
    
    public ObjectId? NetworkId { get; set; }
    public Network? Network { get; set; }
    public ICollection<Store> Stores { get; set; } = new List<Store>();

    public static Result<(IEnumerable<Category>?, ProviderError?)> Create(
        Result<IEnumerable<ICategoryResponse>> response,
        ObjectId networkId
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
                return Result.Failure<(IEnumerable<Category>?, ProviderError?)>(
                    (default, providerError.Value),
                    providerError.Error,
                    providerError.HttpStatusCode
                );
            }
        }

        IEnumerable<Category> categories = response.Value.Select(category => new Category
        {
            Name = category.Name,
            ProviderCategoryId = category.ProviderCategoryId,
            NetworkId = networkId
        }).ToList();

        return Result.Success<(IEnumerable<Category>?, ProviderError?)>((categories, default));
    }
}