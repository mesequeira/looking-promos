using LookingPromos.SharedKernel.Models;
using MediatR;

namespace LookingPromos.WebApi.Application.Abstractions.Messaging;

/// <summary>
/// An implementation for all the queries that requires a pagination.
/// </summary>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public interface IQueryPagination<TResponse> : IRequest<Result<PaginatedResult<TResponse>>>
{
    /// <summary>
    /// Gets the page number.
    /// </summary>
    int PageNumber { get; }

    /// <summary>
    /// Gets the page size.
    /// </summary>
    int PageSize { get; }
}
