using Ride.Domain.ValueObjects;
using Shared.Domain.Primitives;

namespace Ride.Domain.Events;

public record RideRequestedEvent(
    Guid EventId,
    DateTime OccurredOn,
    Guid RideId,
    Guid PassengerId,
    Location PickupLocation,
    Location Destination) : IDomainEvent
{
    public static RideRequestedEvent Create(Guid rideId, Guid passengerId, Location pickupLocation,
        Location destination)
    {
        return new RideRequestedEvent(Guid.NewGuid(), DateTime.Now, rideId, passengerId, pickupLocation, destination);
    }
}