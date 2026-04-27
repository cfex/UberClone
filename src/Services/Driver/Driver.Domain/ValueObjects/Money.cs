using Driver.Domain.Enums;

namespace Driver.Domain.ValueObjects;

public record Money(double Amount, Currency Currency)
{
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