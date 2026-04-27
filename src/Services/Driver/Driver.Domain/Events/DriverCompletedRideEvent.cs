using Shared.Domain.Primitives;

namespace Driver.Domain.Events;

public record DriverCompletedRideEvent(Guid EventId, DateTime OccurredOn, Guid driverId) : IDomainEvent
{
    public static DriverCompletedRideEvent Create(Guid driverId)
    {
        return new DriverCompletedRideEvent(Guid.NewGuid(), DateTime.Now, driverId);
    }
}