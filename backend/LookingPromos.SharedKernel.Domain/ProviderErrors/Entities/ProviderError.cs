using System.Net;
using LookingPromos.SharedKernel.Models;

namespace LookingPromos.SharedKernel.Domain.ProviderErrors.Entities;

public class ProviderError : Entity
{
    public string ProviderEndpoint { get; set; } = default!;

    public string Message { get; set; } = default!;
    
    public static Result<ProviderError> Create(
        Error? response,
        string providerEndpoint
    )
    {
        if (response is null)
        {
            return Result.Failure<ProviderError>(ProviderErrorErrors.ProvideAnEmptyRequest, HttpStatusCode.BadRequest);
        }

        var providerError = new ProviderError
        {
            Message = response.Description,
            ProviderEndpoint = providerEndpoint
        };

        return Result.Success(providerError);
    }
}