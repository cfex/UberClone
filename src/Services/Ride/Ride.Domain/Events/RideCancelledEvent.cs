using Shared.Domain.Primitives;

namespace Ride.Domain.Events;

public record RideCancelledEvent(
    Guid EventId,
    DateTime OccurredOn,
    Guid? DriverId,
    Guid? PassengerId,
    DateTime CancelledOn) : IDomainEvent
{
    public static RideCancelledEvent Create(
        Guid? DriverId,
        Guid? PassengerId,
        DateTime CancelledOn
    )
    {
        return new RideCancelledEvent(Guid.NewGuid(), DateTime.UtcNow, DriverId, PassengerId, CancelledOn);
    }
}