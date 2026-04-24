using MediatR;
using Ride.Application.Abstractions;
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

        // call driver and passenger services if needed to create integration event
        // var availableDriver = _driverGrpcClient.

        // _eventBus.PublishAsync()
    }
}