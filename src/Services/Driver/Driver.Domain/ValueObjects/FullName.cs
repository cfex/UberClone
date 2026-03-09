namespace Driver.Domain.ValueObjects;

public record FullName
{
    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    private string FirstName { get; }
    private string LastName { get; }

    public static FullName Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name cannot be empty.");

        if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name cannot be empty.");

        if (firstName.Length > 50) throw new ArgumentException("First name cannot be longer than 50 characters.");

        if (lastName.Length > 50) throw new ArgumentException("Last name cannot be longer than 50 characters.");

        return new FullName(firstName.Trim(), lastName.Trim());
    }

    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }

    public override string ToString()
    {
        return GetFullName();
    }
}