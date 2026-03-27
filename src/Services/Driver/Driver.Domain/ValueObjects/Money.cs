using System.Text.Json.Serialization;
using Driver.Domain.Enums;

namespace Driver.Domain.ValueObjects;

public record Money
{
    [JsonConstructor]
    private Money()
    {
    }

    private Money(double amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public double Amount { get; }
    public Currency Currency { get; }

    public static Money Create(double amount, Currency currency)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be greater than zero");

        return new Money(amount, currency);
    }

    public override string ToString()
    {
        return $"{Amount:C} {Currency}";
    }
}