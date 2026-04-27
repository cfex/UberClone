using Driver.Domain.ValueObjects;
using Shared.Domain.Primitives;

namespace Driver.Domain.Events;

public record DriverCreatedEvent(
    Guid EventId,
    DateTime OccurredOn,
    Guid DriverId,
    Email Email,
    FullName FullName,
    string Role) : IDomainEvent
{
    public static DriverCreatedEvent Create(Guid driverId, Email email, FullName fullName, string role)
    {
        return new DriverCreatedEvent(Guid.NewGuid(), DateTime.UtcNow, driverId, email, fullName, role);
    }
}