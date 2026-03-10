using Driver.Domain.Enums;
using Driver.Domain.Events;
using Driver.Domain.Primitives;
using Driver.Domain.ValueObjects;

namespace Driver.Domain.Entities;

public class Driver : AggregateRoot
{
    private Driver()
    {
    }

    private Driver(Guid id, FullName fullName, Email email, DriverStatus status, Money fare, Document? document,
        Vehicle? vehicle) : base(id)
    {
        FullName = fullName;
        Email = email;
        Status = status;
        Fare = fare;
        Vehicle = vehicle;
        Document = document;
    }

    public FullName FullName { get; }
    public Email Email { get; }
    private Money Fare { get; }
    private Vehicle? Vehicle { get; }
    private DriverStatus Status { get; set; }
    private Document? Document { get; set; }

    public static Driver Create(Guid id, FullName fullName, Email email, DriverStatus status, Money fare,
        Document? document,
        Vehicle? vehicle)
    {
        var driver = new Driver(id, fullName, email, status, fare, document, vehicle);

        driver.AddDomainEvent(DriverCreatedEvent.Create(driver.Id, email, fullName));
        return driver;
    }

    public void AddDocument(Document document)
    {
        Document = document;
    }

    public void GoOnline()
    {
        if (!Email.IsVerified) throw new Exception("Email is not verified");
        if (Document != null && !Document.IsExpired()) throw new Exception("Document is expired");
        if (Vehicle != null && !Vehicle.IsValid()) throw new Exception("Vehicle is not valid");
        if (Status == DriverStatus.OnRide) throw new Exception("Driver is busy");

        Status = DriverStatus.Online;
        AddDomainEvent(DriverStatusChangedEvent.Create(Id, Status, DriverStatus.Online));
    }

    public void GoOffline()
    {
        if (Status == DriverStatus.OnRide) throw new Exception("Driver is busy");

        Status = DriverStatus.Offline;
        AddDomainEvent(DriverStatusChangedEvent.Create(Id, Status, DriverStatus.Offline));
    }

    public void StartCommuting()
    {
        if (Status == DriverStatus.OnRide) throw new Exception("Driver is busy");
        Status = DriverStatus.Commuting;
        AddDomainEvent(DriverStatusChangedEvent.Create(Id, Status, DriverStatus.Commuting));
    }

    public void StartRide()
    {
        if (Status == DriverStatus.OnRide) throw new Exception("Driver is busy");
        Status = DriverStatus.OnRide;
        AddDomainEvent(DriverStatusChangedEvent.Create(Id, Status, DriverStatus.OnRide));
    }

    public void CompleteRide()
    {
        if (Status != DriverStatus.OnRide) throw new Exception("Driver is not on the ride");
        Status = DriverStatus.Available;
        AddDomainEvent(DriverStatusChangedEvent.Create(Id, Status, DriverStatus.Available));
        AddDomainEvent(DriverCompletedRideEvent.Create(this));
    }

    public override string ToString()
    {
        return $"{Id} - {FullName} - {Vehicle}";
    }
}