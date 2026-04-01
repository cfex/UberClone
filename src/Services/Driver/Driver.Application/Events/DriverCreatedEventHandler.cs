using Driver.Application.Abstractions;
using Driver.Application.IntegrationEvents;
using Driver.Domain.Events;
using Driver.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Driver.Application.Events;

public class DriverCreatedEventHandler : INotificationHandler<DriverDomainEvent<DriverCreatedEvent>>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<DriverCreatedEventHandler> _logger;

    public DriverCreatedEventHandler(ILogger<DriverCreatedEventHandler> logger, IEventBus eventBus,
      IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _eventBus = eventBus;
    }

    public async Task Handle(DriverDomainEvent<DriverCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        var integrationEvent = new UserCreatedIntegrationEvent
        {
            UserId = domainEvent.driverId,
            FirstName = domainEvent.fullName.FirstName,
            LastName = domainEvent.fullName.LastName,
            Email = domainEvent.email.Value,
            Role = domainEvent.role
        };

        await _eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}