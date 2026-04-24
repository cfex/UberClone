using System;

namespace Ride.Domain.Primitives;

public interface IDomainEvent
{
    Guid EventId { get; }
    DateTime OccurredOn { get; }
}