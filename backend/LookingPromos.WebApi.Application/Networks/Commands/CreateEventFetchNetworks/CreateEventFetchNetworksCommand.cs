using LookingPromos.WebApi.Application.Abstractions.Messaging;
using MediatR;

namespace LookingPromos.WebApi.Application.Networks.Commands.CreateEventFetchNetworks;

public class CreateEventFetchNetworksCommand : ITransactionalCommand<Unit>;