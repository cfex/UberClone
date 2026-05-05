using Ride.Domain.ValueObjects;
using Shared.Domain.Primitives;

namespace Ride.Domain.Events;

public record RideCompletedEvent(
    Guid EventId,
    DateTime OccurredOn,
    Guid DriverId,
    Guid PassengerId,
    Money Price,
    DateTime CompletedAt) : IDomainEvent
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