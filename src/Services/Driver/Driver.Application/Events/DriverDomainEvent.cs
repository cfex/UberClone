using MediatR;
using Shared.Domain.Primitives;

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