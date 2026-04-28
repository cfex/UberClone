using System.ComponentModel;
using Driver.Domain.Enums;

namespace Driver.Domain.ValueObjects;

public record Money
{
    private Money()
    {
    }

    public Money(double amount, Currency currency)
    {
        if (!Enum.IsDefined(currency))
            throw new InvalidEnumArgumentException(nameof(currency), (int)currency, typeof(Currency));
        Amount = amount;
        Currency = currency;
    }

    public double Amount { get; }
    public Currency Currency { get; }

    public static Money Create(double amount, Currency currency)
    {
        if (amount < 0) throw new ArgumentException("Amount must be greater or equal to zero");

        return new Money(amount, currency);
    }

    public override string ToString()
    {
        return $"{Amount:C} {Currency}";
    }
}