using Shared.Domain.Primitives;

namespace Ride.Domain.Events;

public record RideStartedEvent(
    Guid EventId,
    DateTime OccurredOn,
    Guid DriverId,
    Guid PassengerId,
    double Price,
    DateTime StartedAt) : IDomainEvent
{
    public static RideStartedEvent Create(Guid driverId, Guid passengerId, double price, DateTime startedAt)
    {
        return new RideStartedEvent(Guid.NewGuid(), DateTime.UtcNow, driverId, passengerId, price, startedAt);
    }
}