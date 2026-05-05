using Driver.Domain.Enums;
using Shared.Domain.Primitives;

namespace Driver.Domain.Events;

public record DriverStatusChangedEvent(
    Guid EventId,
    DateTime OccurredOn,
    Guid DriverId,
    DriverStatus OldStatus,
    DriverStatus NewStatus) : IDomainEvent
{
    public static DriverStatusChangedEvent Create(Guid driverId, DriverStatus oldStatus, DriverStatus newStatus)
    {
        return new DriverStatusChangedEvent(Guid.NewGuid(), DateTime.UtcNow, driverId, oldStatus, newStatus);
    }
}