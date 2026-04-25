using Ride.Domain.Primitives;
using Ride.Domain.ValueObjects;

namespace Ride.Domain.Events;

public record RideRequestedEvent(
    Guid EventId,
    DateTime OccurredOn,
    Guid RideId,
    Guid passengerId,
    Location pickupLocation,
    Location destination) : IDomainEvent
{
    public static RideRequestedEvent Create(Guid rideId, Guid passengerId, Location pickupLocation,
        Location destination)
    {
        return new RideRequestedEvent(Guid.NewGuid(), DateTime.Now, rideId, passengerId, pickupLocation, destination);
    }
}