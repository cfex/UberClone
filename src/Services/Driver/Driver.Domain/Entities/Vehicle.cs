using System.Text.Json.Serialization;
using Driver.Domain.Primitives;

namespace Driver.Domain.Entities;

public class Vehicle : Entity
{
    [JsonConstructor]
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
    public string LicensePlate { get; }
    public string Color { get; }
    public DateTime RegistrationDate { get; }

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
        var oneYearAgo = DateTime.UtcNow.AddYears(-1);
        return RegistrationDate >= oneYearAgo;
    }

    public override string ToString()
    {
        return $"{Make} {Model} - {LicensePlate} - {Color}";
    }
}