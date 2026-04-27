using Ride.Domain.Enums;
using Ride.Domain.Events;
using Ride.Domain.Primitives;
using Ride.Domain.ValueObjects;

namespace Ride.Domain.Entities;

public class Ride : AggregateRoot
{
    private Ride()
    {
    }

    private Ride(Guid? driverId, Guid passengerId, Location pickupLocation, Location destination, RideStatus status,
        Money price, DateTime createdAt)
    {
        DriverId = driverId;
        PassengerId = passengerId;
        PickupLocation = pickupLocation;
        Destination = destination;
        Status = status;
        Price = price;
        CreatedAt = createdAt;
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

    public static Ride Create(Guid? driverId, Guid passengerId, Location pickupLocation, Location destination,
        RideStatus status, DateTime createdAt)
    {
        // Approximate price will be calculated in the driver service.
        // When the driver accept the ride, the total price will be calculated based on he's fare and persisted.
        var ridePrice = Money.Create(0, Currency.EUR);
        var ride = new Ride(driverId, passengerId, pickupLocation, destination, status, ridePrice, createdAt);

        ride.AddDomainEvent(RideRequestedEvent.Create(ride.Id, ride.PassengerId, ride.PickupLocation,
            ride.Destination));

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