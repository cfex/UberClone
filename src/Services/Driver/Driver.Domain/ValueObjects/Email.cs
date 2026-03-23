namespace Driver.Domain.ValueObjects;

public record Email
{
    private Email(string value, bool isVerified)
    {
        Value = value;
        IsVerified = isVerified;
    }

    public string Value { get; private set; }
    public bool IsVerified { get; private set; }


    public static Email Create(string value, bool isVerified = false)
    {
        return new Email(value, isVerified);
    }
}