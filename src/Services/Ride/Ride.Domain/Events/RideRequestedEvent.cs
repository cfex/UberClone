using Ride.Domain.Primitives;
using Ride.Domain.ValueObjects;

namespace Ride.Domain.Events;

public record RideRequestedEvent(
    Guid EventId,
    DateTime OccurredOn,
    Guid passengerId,
    Location pickupLocation,
    Location destination) : IDomainEvent
{
    public static RideRequestedEvent Create(Guid passengerId, Location pickupLocation, Location destination)
    {
        return new RideRequestedEvent(Guid.NewGuid(), DateTime.Now, passengerId, pickupLocation, destination);
    }
}