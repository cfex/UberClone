using Ride.Domain.Enums;

namespace Ride.Domain.ValueObjects;

public record Money
{
    private readonly double _amount;
    private readonly Currency _currency;

    private Money(double amount, Currency currency)
    {
        _amount = amount;
        _currency = currency;
    }

    public static Money Create(double amount, Currency currency)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be greater than zero");

        return new Money(amount, currency);
    }

    public override string ToString()
    {
        return $"{_amount:C} {_currency}";
    }
}