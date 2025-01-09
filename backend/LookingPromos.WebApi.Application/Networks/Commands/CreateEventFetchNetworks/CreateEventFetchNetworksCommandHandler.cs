using System.Net;
using ClaLookingPromos.SharedKernel.Contracts.Networks.Events;
using LookingPromos.SharedKernel.Domain.Networks.Repositories;
using LookingPromos.SharedKernel.Models;
using MassTransit;
using MediatR;

namespace LookingPromos.WebApi.Application.Networks.Commands.CreateEventFetchNetworks;

internal sealed class CreateEventFetchNetworksCommandHandler(
    IBus publisher,
    INetworkRepository networkRepository
) : IRequestHandler<CreateEventFetchNetworksCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(CreateEventFetchNetworksCommand request, CancellationToken cancellationToken)
    {
        var networks = await networkRepository.GetAllAsync(cancellationToken);

        if (!networks.Any())
        {
            return Result.Failure<Unit>(
                new Error("NotFoundNetworks", "We try to found any network in the database but we didn't found any.",
                    ErrorType.NotFound), HttpStatusCode.NotFound);
        }

        var messages = networks.Select(network => new GetNetworksEvent(network.Id));

        await publisher.PublishBatch(messages, cancellationToken);

        return Result.Success(new Unit(), HttpStatusCode.NoContent);
    }
}