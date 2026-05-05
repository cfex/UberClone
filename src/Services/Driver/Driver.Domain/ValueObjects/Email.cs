using System.Text.Json.Serialization;

namespace Driver.Domain.ValueObjects;

public record Email(string Value, bool IsVerified)
{
    public string Value { get; private set; } = Value;
    public bool IsVerified { get; private set; } = IsVerified;


    public static Email Create(string value, bool isVerified = false)
    {
        return new Email(value, isVerified);
    }
}