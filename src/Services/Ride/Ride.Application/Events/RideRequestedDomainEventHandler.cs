using MediatR;
using Ride.Application.Abstractions;
using Ride.Domain.Events;
using Shared.Contracts.IntegrationEvents.Ride;

namespace Ride.Application.Events;

public class RideRequestedDomainEventHandler : INotificationHandler<RideDomainEvent<RideRequestedEvent>>
{
    private const double DefaultRadius = 5;
    private readonly IDriverGrpcClient _driverGrpcClient;
    private readonly IEventBus _eventBus;
    private readonly ILocationGrpcClient _locationGrpcClient;

    public RideRequestedDomainEventHandler(IDriverGrpcClient driverGrpcClient, IEventBus eventBus,
        ILocationGrpcClient locationGrpcClient)
    {
        _driverGrpcClient = driverGrpcClient;
        _eventBus = eventBus;
        _locationGrpcClient = locationGrpcClient;
    }

    public async Task Handle(RideDomainEvent<RideRequestedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        var drivers = await _locationGrpcClient.GetNearestDriversAsync(domainEvent.PickupLocation.Longitude,
            domainEvent.PickupLocation.Latitude, DefaultRadius);

        // TODO: Handle this case - no nearest drivers.
        if (drivers.Count == 0)
            return;

        var driverIds = drivers.Select(x => x.DriverId).ToList();
        var availableDrivers = await _driverGrpcClient.GetAvailableDriversAsync(driverIds);

        // TODO: Handle this case - no available drivers
        if (availableDrivers.Count == 0)
            return;

        var availableDriverIds = availableDrivers.Select(x => Guid.Parse(x.DriverId)).ToList();

        var integrationEvent = new RideRequestedDispatchEvent
        {
            RideId = domainEvent.RideId,
            DestinationLatitude = domainEvent.Destination.Latitude,
            DestinationLongitude = domainEvent.Destination.Longitude,
            PickupLatitude = domainEvent.PickupLocation.Latitude,
            PickupLongitude = domainEvent.PickupLocation.Longitude,
            DriverIds = availableDriverIds,
            OccurredOn = domainEvent.OccurredOn,
            PassengerId = domainEvent.PassengerId
        };

        await _eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}