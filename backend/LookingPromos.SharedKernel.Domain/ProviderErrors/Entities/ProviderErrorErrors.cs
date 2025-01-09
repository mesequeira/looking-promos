using LookingPromos.SharedKernel.Models;

namespace LookingPromos.SharedKernel.Domain.ProviderErrors.Entities;

public static class ProviderErrorErrors
{
    public static Error ProvideAnEmptyRequest => 
        Error.Conflict(nameof(ProvideAnEmptyRequest), "The user provided an empty request.");
}