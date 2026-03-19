using Driver.Domain.Primitives;
using Driver.Domain.ValueObjects;

namespace Driver.Domain.Events;

public record DriverCreatedEvent(
    Guid EventId,
    DateTime OccurredOn,
    Guid driverId,
    Email email,
    FullName fullName,
    string role) : IDomainEvent
{
    public static DriverCreatedEvent Create(Guid driverId, Email email, FullName fullName, string role)
    {
        return new DriverCreatedEvent(Guid.NewGuid(), DateTime.UtcNow, driverId, email, fullName, role);
    }
}