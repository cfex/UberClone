using MediatR;
using Ride.Application.Abstractions;
using Ride.Domain.Events;
using Ride.Domain.Repositories;

namespace Ride.Application.Events;

public class RideRequestedDomainEventHandler : INotificationHandler<RideDomainEvent<RideRequestedEvent>>
{
    private readonly IEventBus _eventBus;
    private readonly IRideRepository _rideRepository;

    public async Task Handle(RideDomainEvent<RideRequestedEvent> notification, CancellationToken cancellationToken)
    {

        var domainEvent = notification.DomainEvent;
        
        // call driver and passenger services if needed to create integration event
        // var driver = await _rideRepository.
        // _eventBus.PublishAsync()
    }
}