using MediatR;
using Ride.Application.Abstractions;
using Ride.Domain.Events;
using Shared.Contracts.IntegrationEvents.Ride;

namespace Ride.Application.Events;

public class RideRequestedDomainEventHandler : INotificationHandler<RideDomainEvent<RideRequestedEvent>>
{
    private readonly IDriverGrpcClient _driverGrpcClient;
    private readonly IEventBus _eventBus;

    public RideRequestedDomainEventHandler(IDriverGrpcClient driverGrpcClient, IEventBus eventBus)
    {
        _driverGrpcClient = driverGrpcClient;
        _eventBus = eventBus;
    }

    public async Task Handle(RideDomainEvent<RideRequestedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        var availableDriver = await _driverGrpcClient.GetAvailableDriversAsync(domainEvent.PickupLocation);
        var driverIds = availableDriver.Select(x => Guid.Parse(x.DriverId)).ToList();

        var integrationEvent = new RideRequestedDispatchEvent
        {
            RideId = domainEvent.RideId,
            DestinationLatitude = domainEvent.Destination.Latitude,
            DestinationLongitude = domainEvent.Destination.Longitude,
            PickupLatitude = domainEvent.PickupLocation.Latitude,
            PickupLongitude = domainEvent.PickupLocation.Longitude,
            DriverIds = driverIds,
            OccurredOn = domainEvent.OccurredOn,
            PassengerId = domainEvent.PassengerId
        };

        await _eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}