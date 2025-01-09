using System.Net;
using System.Net.Http.Json;
using ClaLookingPromos.SharedKernel.Contracts.Galicia.Requests;
using ClaLookingPromos.SharedKernel.Contracts.Galicia.Responses;
using LookingPromos.SharedKernel.Domain.Galicia.Services;
using LookingPromos.SharedKernel.Models;

namespace LookingPromos.SharedKernel.Infrastructure.Galicia.Services;

public class GaliciaService(
    IHttpClientFactory factory
) : IGaliciaService
{
    private readonly HttpClient _client = factory.CreateClient(nameof(GaliciaService));

    public async Task<Result<GetGaliciaCategoriesResponse>> GetCategoriesAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _client.GetAsync("categorias?Categoria=true&SubCategoria=false&Visibles=true",
                cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                GetGaliciaCategoriesResponse? content =
                    await response.Content.ReadFromJsonAsync<GetGaliciaCategoriesResponse>(cancellationToken);

                if (content is not null)
                {
                    return Result.Success(content);
                }

                return Result.Failure<GetGaliciaCategoriesResponse>(
                    new Error("ErrorDeserializingGetCategoriesResponse",
                        "Error deserializing the response of the get categories request", ErrorType.Conflict),
                    HttpStatusCode.BadRequest);
            }

            return Result.Failure<GetGaliciaCategoriesResponse>(
                new Error("ErrorGettingCategories",
                    "Error getting the categories from the Galicia API", ErrorType.Conflict),
                response.StatusCode);
        }
        catch (Exception ex)
        {
            return Result.Failure<GetGaliciaCategoriesResponse>(
                new Error("ErrorGettingCategories",
                    "Error getting the categories from the Galicia API", ErrorType.Conflict),
                HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<GetGaliciaStoresResponse>> GetStoresAsync(
        GetGaliciaStoresRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _client.GetAsync(
                $"agrupador-promocion?pageIndex=1&pageSize=1000&IdCategoria={request.ProviderCategoryId}",
                cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return Result.Failure<GetGaliciaStoresResponse>(
                    new Error("ErrorGettingStores",
                        $"Error getting the stores associated to the category {request.ProviderCategoryId} from the Galicia API",
                        ErrorType.Conflict),
                    HttpStatusCode.InternalServerError);
            }

            GetGaliciaStoresResponse? content =
                await response.Content.ReadFromJsonAsync<GetGaliciaStoresResponse>(cancellationToken);

            if (content is null)
            {
                return Result.Failure<GetGaliciaStoresResponse>(
                    new Error("ErrorDeserializingGetStoresResponse",
                        $"An error occurred deserializing the response of the get stores request for the category {request.ProviderCategoryId}",
                        ErrorType.Conflict),
                    HttpStatusCode.InternalServerError);
            }

            return Result.Success(content);
        }
        catch (Exception)
        {
            // TODO: Log the exception
            return Result.Failure<GetGaliciaStoresResponse>(
                new Error("ErrorGettingStores",
                    $"Error getting the stores associated to the category {request.ProviderCategoryId} from the Galicia API",
                    ErrorType.Conflict),
                HttpStatusCode.InternalServerError);
        }
    }
}