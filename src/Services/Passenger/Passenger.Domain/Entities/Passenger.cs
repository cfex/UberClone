using Passenger.Domain.Enums;
using Passenger.Domain.ValueObjects;
using Shared.Domain.Primitives;

namespace Passenger.Domain.Entities;

public class Passenger : AggregateRoot
{
    private Passenger()
    {
    }

    private Passenger(Guid id, FullName fullName, Email email, PassengerStatus status)
    {
        PassengerId = id;
        FullName = fullName;
        email = email;
        status = status;
    }

    public Guid PassengerId { get; private set; }
    public FullName FullName { get; private set; }
    public PassengerStatus Status { get; private set; }

    public Email Email { get; private set; }

    public static Passenger Create(Guid id, FullName fullName, Email email, PassengerStatus status)
    {
        return new Passenger(id, fullName, email, status);
    }
}