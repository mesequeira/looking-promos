using LookingPromos.SharedKernel.Models;

namespace LookingPromos.SharedKernel.Domain.Categories.DomainEvents;

public record CategoryCreatedDomainEvent(long Id) : IDomainEvent;