using MediatR;
using Ride.Application.Abstractions;
using Ride.Application.Events.IntegrationEvents;
using Ride.Domain.Events;

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

        var availableDriver = await _driverGrpcClient.GetAvailableDriversAsync(domainEvent.pickupLocation);
        var driverIds = availableDriver.Select(x => Guid.Parse(x.DriverId)).ToList();

        var integrationEvent = new RideRequestedDispatchEvent
        {
            RideId = domainEvent.RideId,
            Destination = domainEvent.destination,
            Location = domainEvent.pickupLocation,
            DriverIds = driverIds,
            RequestedAt = DateTime.UtcNow,
            CreatedAt = domainEvent.OccurredOn
        };

        await _eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}