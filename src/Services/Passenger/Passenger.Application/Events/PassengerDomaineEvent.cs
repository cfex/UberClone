using MediatR;
using Shared.Domain.Primitives;

namespace Passenger.Application.Events;

public class PassengerDomaineEvent<TDomainEvent> : INotification
    where TDomainEvent : IDomainEvent
{
    public PassengerDomaineEvent(TDomainEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }

    public TDomainEvent DomainEvent { get; }
}