using Shared.Domain.Primitives;

namespace Driver.Domain.Entities;

public class Vehicle : Entity
{
    private Vehicle()
    {
    }

    private Vehicle(string make, string model, string licensePlate, string color, DateTime registrationDate) : base(
        Guid.NewGuid())
    {
        Make = make;
        Model = model;
        LicensePlate = licensePlate;
        Color = color;
        RegistrationDate = registrationDate;
    }

    public string Make { get; }
    public string Model { get; }
    public string LicensePlate { get; private set; }
    public string Color { get; private set; }
    public DateTime RegistrationDate { get; private set; }

    public static Vehicle Create(string make, string model, string licensePlate, string color, DateTime registeredUntil)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(make);
        ArgumentException.ThrowIfNullOrWhiteSpace(model);
        ArgumentException.ThrowIfNullOrWhiteSpace(licensePlate);
        ArgumentException.ThrowIfNullOrWhiteSpace(color);

        return new Vehicle(make.Trim(), model.Trim(), licensePlate.Trim(), color.Trim(), registeredUntil);
    }

    public void UpdateColor(string color)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(color);
        Color = color;
    }

    public void RenewRegistration()
    {
        RegistrationDate = DateTime.UtcNow.AddYears(1);
    }

    public void UpdateLicencePlate(string licencePlate)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(licencePlate);
        LicensePlate = licencePlate;
    }

    public bool IsValid()
    {
        var oneYearAgo = DateTime.UtcNow.AddYears(-1);
        return RegistrationDate >= oneYearAgo;
    }

    public override string ToString()
    {
        return $"{Make} {Model} - {LicensePlate} - {Color}";
    }
}