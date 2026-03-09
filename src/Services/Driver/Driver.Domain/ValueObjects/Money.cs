using Driver.Domain.Enums;

namespace Driver.Domain.ValueObjects;

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
        return new Money(amount, currency);
    }

    public override string ToString()
    {
        return $"{_amount:C} {_currency}";
    }
}