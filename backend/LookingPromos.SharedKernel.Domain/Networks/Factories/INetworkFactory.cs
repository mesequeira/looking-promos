using LookingPromos.SharedKernel.Domain.Networks.Entities;

namespace LookingPromos.SharedKernel.Domain.Networks.Factories;

public interface INetworkFactory
{
    Task<bool> CreateStrategyAsync(Network network, CancellationToken cancellationToken = default);
}