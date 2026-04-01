using Driver.Domain.Primitives;
using Ride.Domain.Enums;
using Ride.Domain.Events;
using Ride.Domain.ValueObjects;

namespace Ride.Domain.Entities;

public class Ride : AggregateRoot
{
    private Ride()
    {
    }

    public Ride(Guid driverId, Guid passengerId, Location pickupLocation, Location destination, RideStatus status)
    {
        DriverId = driverId;
        PassengerId = passengerId;
        PickupLocation = pickupLocation;
        Destination = destination;
        Status = status;
    }

    public Guid? DriverId { get; private set; }
    public Guid PassengerId { get; init; }
    public Location PickupLocation { get; init; }
    public Location Destination { get; }
    public RideStatus Status { get; private set; }
    public Money Price { get; private set; }
    public DateTime CreatedAt { get; init; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    public static Ride Create(Guid driverId, Guid passengerId, Location pickupLocation, Location destination,
        RideStatus status)
    {
        var ride = new Ride(driverId, passengerId, pickupLocation, destination, status);

        ride.AddDomainEvent(RideRequestedEvent.Create(ride.PassengerId, ride.PickupLocation, ride.Destination));

        return ride;
    }

    public void AssignDriver(Guid driverId)
    {
        DriverId = driverId;
        // AddDomainEvent();
    }

    public void StartRide()
    {
        Status = RideStatus.InProgress;
        StartedAt = DateTime.UtcNow;
        // AddDomainEvent();
    }

    public void CompleteRide()
    {
        Status = RideStatus.Completed;
        var now = DateTime.UtcNow;
        CompletedAt = now;
        AddDomainEvent(RideCompletedEvent.Create(DriverId!.Value, PassengerId, Price, now));
    }

    public void CancelRide()
    {
        Status = RideStatus.Cancelled;
        // AddDomainEvent();
    }

    public void SetFinalPrice(Money price)
    {
        Price = price;
    }
}