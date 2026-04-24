using Ride.Domain.Primitives;
using Ride.Domain.ValueObjects;

namespace Ride.Domain.Events;

public record RideCompletedEvent(
    Guid EventId,
    DateTime OccurredOn,
    Guid driverId,
    Guid passengerId,
    Money price,
    DateTime completedAt) : IDomainEvent
{
    public static RideCompletedEvent Create(
        Guid driverId,
        Guid passengerId,
        Money price,
        DateTime completedAt)
    {
        return new RideCompletedEvent(Guid.NewGuid(), DateTime.UtcNow, driverId, passengerId, price, completedAt);
    }
}