using System.Net;
using ClaLookingPromos.SharedKernel.Contracts.Networks.Events;
using LookingPromos.SharedKernel.Domain.Networks.Entities;
using LookingPromos.SharedKernel.Domain.Networks.Factories;
using LookingPromos.SharedKernel.Domain.Networks.Repositories;
using LookingPromos.SharedKernel.Models;
using MassTransit;
using MongoDB.Bson;

namespace LookingPromos.Worker.Application.Networks.Consumers;

public sealed class GetNetworksEventConsumer(
    IUnitOfWork unitOfWork,
    INetworkRepository networkRepository,
    INetworkFactory factory
) : IConsumer<GetNetworksEvent>
{
    public async Task Consume(ConsumeContext<GetNetworksEvent> context)
    {
        var networkId = context.Message.NetworkId;

        var network = await networkRepository.GetByIdAsync(ObjectId.Parse(networkId), context.CancellationToken);

        if (network == null)
        {
            var error = new Error("Network not found", $"Network with id {networkId} not found", ErrorType.Failure);

            await context.RespondAsync(Result.Failure(error, HttpStatusCode.NotFound));

            return;
        }

        await factory.CreateStrategyAsync(network, context.CancellationToken);

        await unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}