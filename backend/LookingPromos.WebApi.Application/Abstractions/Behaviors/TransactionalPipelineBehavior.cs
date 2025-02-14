using System.Data;
using LookingPromos.SharedKernel.Models;
using LookingPromos.WebApi.Application.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LookingPromos.WebApi.Application.Abstractions.Behaviors;

/// <summary>
/// A pipeline behavior that wraps the workflow of the request inside a transaction to ensure the atomicity of the operation.
/// </summary>
/// <param name="unitOfWork">The unit of work to handle the transaction.</param>
/// <param name="logger">The logger to log the processing of the request.</param>
/// <typeparam name="TRequest">The incoming request type.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
internal sealed class TransactionalPipelineBehavior<TRequest, TResponse>(
    IUnitOfWork unitOfWork,
    ILogger<TransactionalPipelineBehavior<TRequest, TResponse>> logger
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseTransactionCommand
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Beginning transaction for {RequestName}", typeof(TRequest).Name);

        using IDbTransaction transaction = await unitOfWork.BeginTransactionAsync(
            cancellationToken
        );

        try
        {
            TResponse response = await next();

            transaction.Commit();

            logger.LogInformation("Committed transaction for {RequestName}", typeof(TRequest).Name);

            return response;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            logger.LogError(ex, "Rollback transaction for {RequestName}", typeof(TRequest).Name);
            throw;
        }
    }
}

