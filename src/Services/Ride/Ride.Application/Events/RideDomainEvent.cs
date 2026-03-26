using Driver.Domain.Primitives;
using MediatR;

namespace Ride.Application.Events;

public class RideDomainEvent<TDomainEvent> : INotification
    where TDomainEvent : IDomainEvent
{
    public RideDomainEvent(TDomainEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }

    public TDomainEvent DomainEvent { get; }
}