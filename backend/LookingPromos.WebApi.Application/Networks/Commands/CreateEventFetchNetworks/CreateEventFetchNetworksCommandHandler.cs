using System.Net;
using ClaLookingPromos.SharedKernel.Contracts.Categories.Events;
using ClaLookingPromos.SharedKernel.Contracts.Networks.Events;
using LookingPromos.SharedKernel.Domain.Networks.Repositories;
using LookingPromos.SharedKernel.Models;
using MassTransit;
using MediatR;
using MongoDB.Bson;

namespace LookingPromos.WebApi.Application.Networks.Commands.CreateEventFetchNetworks;

internal sealed class CreateEventFetchNetworksCommandHandler(
    IBus publisher,
    INetworkRepository networkRepository
) : IRequestHandler<CreateEventFetchNetworksCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(CreateEventFetchNetworksCommand request, CancellationToken cancellationToken)
    {

        CategoryCreatedEvent mensaje = new CategoryCreatedEvent("678079347965d52d6d8b6bdc");
        
        await publisher.Publish(mensaje, cancellationToken);

        return Result.Success(new Unit(), HttpStatusCode.NoContent);
        
        var networks = await networkRepository.GetAsync(cancellationToken);

        if (networks is { Count: 0 })
        {
            return Result.Failure<Unit>(
                new Error("NotFoundNetworks", "We try to found any network in the database but we didn't found any.",
                    ErrorType.NotFound), HttpStatusCode.NotFound);
        }

        var messages = networks.Select(network => new GetNetworksEvent(network.Id.ToString()));

        await publisher.PublishBatch(messages, cancellationToken);

        return Result.Success(new Unit(), HttpStatusCode.NoContent);
    }
}