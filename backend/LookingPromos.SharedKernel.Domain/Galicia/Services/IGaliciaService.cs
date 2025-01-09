using ClaLookingPromos.SharedKernel.Contracts.Galicia.Requests;
using ClaLookingPromos.SharedKernel.Contracts.Galicia.Responses;
using LookingPromos.SharedKernel.Models;

namespace LookingPromos.SharedKernel.Domain.Galicia.Services;

public interface IGaliciaService
{
    Task<Result<GetGaliciaCategoriesResponse>> GetCategoriesAsync(CancellationToken cancellationToken = default);

    Task<Result<GetGaliciaStoresResponse>> GetStoresAsync(
        GetGaliciaStoresRequest request,
        CancellationToken cancellationToken = default);
}