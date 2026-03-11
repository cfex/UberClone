using Driver.Domain.Primitives;
using MediatR;

namespace Driver.Application.Events;

public class DriverDomainEvent<TDomainEvent> : INotification
    where TDomainEvent : IDomainEvent
{
    public DriverDomainEvent(TDomainEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }

    public TDomainEvent DomainEvent { get; }
}