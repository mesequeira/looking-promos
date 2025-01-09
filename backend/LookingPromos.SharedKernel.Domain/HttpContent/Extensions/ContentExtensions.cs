using System.Net;
using ClaLookingPromos.SharedKernel.Contracts.Categories.Interfaces;
using ClaLookingPromos.SharedKernel.Contracts.Stores;
using LookingPromos.SharedKernel.Domain.HttpContent.Entities;
using LookingPromos.SharedKernel.Models;

namespace LookingPromos.SharedKernel.Domain.HttpContent.Extensions;

public static class ContentExtensions
{
    public static async Task<Result<IEnumerable<IStoreResponse>>> HandleResponseAsync<T>(
        this Task<Result<T>> response,
        Func<T, IEnumerable<IStoreResponse>> converter
    )
    {
        var content = await response;
        
        try
        {
            if (content.IsFailure)
            {
                // Propagar el fallo manteniendo el error original
                return Result.Failure<IEnumerable<IStoreResponse>>(
                    content.Error,
                    content.HttpStatusCode
                );
            }

            // Aplicar la conversión en caso de éxito
            var result = converter(content.Value);
            return Result.Success(result);
        }
        catch
        {
            return Result.Failure<IEnumerable<IStoreResponse>>(
                HttpContentErrors.SomethingGoesWrongReadingTheResponse(typeof(T)),
                HttpStatusCode.InternalServerError);
        }
    }
    
    public static async Task<Result<IEnumerable<ICategoryResponse>>> HandleResponseAsync<T>(
        this Task<Result<T>> response,
        Func<T, IEnumerable<ICategoryResponse>> converter
    )
    {
        var content = await response;
        
        try
        {
            if (content.IsFailure)
            {
                // Propagar el fallo manteniendo el error original
                return Result.Failure<IEnumerable<ICategoryResponse>>(
                    content.Error,
                    content.HttpStatusCode
                );
            }

            // Aplicar la conversión en caso de éxito
            var result = converter(content.Value);
            return Result.Success(result);
        }
        catch
        {
            return Result.Failure<IEnumerable<ICategoryResponse>>(
                HttpContentErrors.SomethingGoesWrongReadingTheResponse(typeof(T)),
                HttpStatusCode.InternalServerError);
        }
    }
}