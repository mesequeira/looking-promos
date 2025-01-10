using System.Net;
using ClaLookingPromos.SharedKernel.Contracts.Categories.Events;
using ClaLookingPromos.SharedKernel.Contracts.Categories.Interfaces;
using ClaLookingPromos.SharedKernel.Contracts.Galicia.Requests;
using ClaLookingPromos.SharedKernel.Contracts.Stores;
using LookingPromos.SharedKernel.Domain.Categories.Entities;
using LookingPromos.SharedKernel.Domain.Categories.Repositories;
using LookingPromos.SharedKernel.Domain.Galicia.Services;
using LookingPromos.SharedKernel.Domain.HttpContent.Extensions;
using LookingPromos.SharedKernel.Domain.Networks.Enums;
using LookingPromos.SharedKernel.Domain.Networks.Strategies;
using LookingPromos.SharedKernel.Domain.ProviderErrors.Entities;
using LookingPromos.SharedKernel.Domain.Stores.Entities;
using LookingPromos.SharedKernel.Domain.Stores.Repositories;
using LookingPromos.SharedKernel.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace LookingPromos.SharedKernel.Infrastructure.Galicia.Strategies;

public class GaliciaStrategy(
    IGaliciaService galiciaService,
    ICategoryRepository categoryRepository,
    IStoreRepository storeRepository,
    IUnitOfWork unitOfWork,
    IBus publisher,
    ILogger<GaliciaStrategy> logger
) : INetworkStrategy
{
    public async Task<bool> GetNetworkPromotionsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            Result<IEnumerable<ICategoryResponse>> getCategoriesResponse = await galiciaService
                .GetCategoriesAsync(cancellationToken)
                .HandleResponseAsync(response => response.Data.List);


            Result<(IEnumerable<Category>?, ProviderError?)> categoriesResult = Category.Create(
                getCategoriesResponse,
                ObjectId.Parse(NetworkVariants.Galicia)
            );

            if (categoriesResult is { IsFailure: true, HttpStatusCode: > HttpStatusCode.MultipleChoices } ||
                categoriesResult.Value.Item1 is null)
            {
                logger.LogError(
                    "Fuimos a buscar las categorías en la red Galicia, pero ocurrió un error conectándonos al servicio.");
                return false;
            }

            IEnumerable<Category> categories = categoriesResult.Value.Item1.ToList();

            await categoryRepository.InsertAsync(categories, cancellationToken);
            
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var events = categories
                .Select(category => new CategoryCreatedEvent(category.Id.ToString()));

            await publisher.PublishBatch(events, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Mientras intentábamos sincronizar los nuevos datos de las categorías y tiendas en la red Galicia, ocurrió un error. {ex}",
                ex);
        }

        return false;
    }

    public async Task<bool> GetStoresAssociatedWithCategoryAsync(Category category,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new GetGaliciaStoresRequest(category.ProviderCategoryId ?? 0);

            Result<IEnumerable<IStoreResponse>> getStoresResponse = await galiciaService
                .GetStoresAsync(request, cancellationToken)
                .HandleResponseAsync(response => response.Data.List);

            Result<(IEnumerable<Store>?, ProviderError?)> storesResult = Store.Create(
                getStoresResponse,
                category.Id
            );

            if (storesResult is { IsFailure: true, HttpStatusCode: > HttpStatusCode.MultipleChoices } ||
                storesResult.Value.Item1 is null)
            {
                logger.LogError(
                    "Fuimos a buscar las tiendas asociadas a la categoría {categoryId} en la red Galicia, pero ocurrió un error conectándonos al servicio.",
                    category.Id);
                return false;
            }

            IEnumerable<Store> stores = storesResult.Value.Item1.ToList();

            await storeRepository.InsertAsync(stores, cancellationToken);
            
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Mientras intentábamos sincronizar los nuevos datos de las tiendas de la categoría {categoryId} la red Galicia, ocurrió un error. {ex}",
                category.Id, ex);
        }

        return false;
    }
}