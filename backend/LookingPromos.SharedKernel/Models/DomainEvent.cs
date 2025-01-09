using MediatR;

namespace LookingPromos.SharedKernel.Models;

/// <summary>
/// The interface that all domain events should inherit from.
/// </summary>
public interface IDomainEvent : INotification;
