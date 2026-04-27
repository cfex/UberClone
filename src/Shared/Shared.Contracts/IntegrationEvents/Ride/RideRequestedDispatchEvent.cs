namespace Shared.Contracts.IntegrationEvents.Ride;

public record RideRequestedDispatchEvent : IntegrationEvent
{
    public required Guid RideId { get; init; }
    public required Guid PassengerId { get; init; }
    public required List<Guid> DriverIds { get; init; }
    public required double PickupLongitude { get; init; }
    public required double PickupLatitude { get; init; }
    public required double DestinationLongitude { get; init; }
    public required double DestinationLatitude { get; init; }
}