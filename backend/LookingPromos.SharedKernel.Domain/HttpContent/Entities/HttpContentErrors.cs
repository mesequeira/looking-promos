using LookingPromos.SharedKernel.Models;

namespace LookingPromos.SharedKernel.Domain.HttpContent.Entities;

public static class HttpContentErrors
{
    public static Error CanNotDeserializeContent(Type type)
    {
        return new Error(nameof(CanNotDeserializeContent),
            $"We found with a problem trying to deserialize the content of type {type.Name}",
            ErrorType.Conflict);
    }
    
    public static Error SomethingGoesWrongReadingTheResponse(Type type)
    {
        return new Error(nameof(SomethingGoesWrongReadingTheResponse),
            "Something goes wrong reading the response of type {type.Name}",
            ErrorType.Conflict);
    }
}