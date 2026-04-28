using Driver.Application.Abstractions;
using Driver.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Contracts.IntegrationEvents.Driver;

namespace Driver.Application.Events;

public class DriverCreatedEventHandler : INotificationHandler<DriverDomainEvent<DriverCreatedEvent>>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<DriverCreatedEventHandler> _logger;

    public DriverCreatedEventHandler(ILogger<DriverCreatedEventHandler> logger, IEventBus eventBus)
    {
        _logger = logger;
        _eventBus = eventBus;
    }

    public async Task Handle(DriverDomainEvent<DriverCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        var integrationEvent = new DriverCreatedIntegrationEvent
        {
            DriverId = domainEvent.DriverId,
            FirstName = domainEvent.FullName.FirstName,
            LastName = domainEvent.FullName.LastName,
            Email = domainEvent.Email.Value,
            Role = domainEvent.Role
        };

        await _eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}