using Driver.Domain.Primitives;

namespace Driver.Domain.Entities;

public class Vehicle : Entity
{
    private Vehicle(string make, string model, string licensePlate, string color, DateTime registeredUntil) : base(
        Guid.NewGuid())
    {
        Make = make;
        Model = model;
        LicensePlate = licensePlate;
        Color = color;
        RegisteredUntil = registeredUntil;
    }

    private string Make { get; }
    private string Model { get; }
    private string LicensePlate { get; }
    private string Color { get; }
    private DateTime RegisteredUntil { get; }

    public static Vehicle Create(string make, string model, string licensePlate, string color, DateTime registeredUntil)
    {
        if (string.IsNullOrWhiteSpace(make)) throw new ArgumentException("Make cannot be empty.");

        if (string.IsNullOrWhiteSpace(model)) throw new ArgumentException("Model cannot be empty.");

        if (string.IsNullOrWhiteSpace(licensePlate)) throw new ArgumentException("License plate cannot be empty.");

        if (string.IsNullOrWhiteSpace(color)) throw new ArgumentException("Color cannot be empty.");

        return new Vehicle(make.Trim(), model.Trim(), licensePlate.Trim(), color.Trim(), registeredUntil);
    }

    public bool IsValid()
    {
        return RegisteredUntil >= DateTime.Now;
    }

    public override string ToString()
    {
        return $"{Make} {Model} - {LicensePlate} - {Color}";
    }
}