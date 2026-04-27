using Driver.Application.Abstractions;
using Driver.Domain.Events;
using MediatR;
using Shared.Contracts.IntegrationEvents.Driver;

namespace Driver.Application.Events;

public class DriverStatusChangedEventHandler : INotificationHandler<DriverDomainEvent<DriverStatusChangedEvent>>
{
    private readonly IEventBus _eventBus;

    public DriverStatusChangedEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(DriverDomainEvent<DriverStatusChangedEvent> notification,
        CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        var integrationEvent = new DriverStatusChangedIntegrationEvent
        {
            DriverId = domainEvent.DriverId,
            OldStatus = domainEvent.OldStatus.ToString(),
            NewStatus = domainEvent.NewStatus.ToString(),
            OccurredOn = domainEvent.OccurredOn
        };

        await _eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}