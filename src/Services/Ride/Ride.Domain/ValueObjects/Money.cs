using System.ComponentModel;
using Ride.Domain.Enums;

namespace Ride.Domain.ValueObjects;

public record Money
{
    private Money()
    {
    }

    public Money(decimal amount, Currency currency)
    {
        if (!Enum.IsDefined(typeof(Currency), currency))
            throw new InvalidEnumArgumentException(nameof(currency), (int)currency, typeof(Currency));
        Amount = amount;
        Currency = currency;
    }

    public decimal Amount { get; }
    public Currency Currency { get; }

    public static Money Create(decimal amount, Currency currency)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be greater than zero");

        return new Money(amount, currency);
    }

    public override string ToString()
    {
        return $"{Amount:C} {Currency}";
    }
}