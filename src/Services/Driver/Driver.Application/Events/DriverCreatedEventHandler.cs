using Driver.Domain.Events;
using MediatR;

namespace Driver.Application.Events;

public class DriverCreatedEventHandler : INotificationHandler<DriverDomainEvent<DriverCreatedEvent>>
{
    public Task Handle(DriverDomainEvent<DriverCreatedEvent> notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}