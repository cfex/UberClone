using Driver.Domain.Primitives;

namespace Driver.Domain.Events;

public record DriverCompletedRideEvent(Guid EventId, DateTime OccurredOn, Entities.Driver driver) : IDomainEvent
{
    // TODO: Pass the DTO instead of Driver obj
    public static DriverCompletedRideEvent Create(Entities.Driver driver)
    {
        return new DriverCompletedRideEvent(Guid.NewGuid(), DateTime.Now, driver);
    }
}