using Sovcombank.FinancialTrading.Domain.Account.Exceptions;

namespace Sovcombank.FinancialTrading.Domain.Account.ValueObjects;

public sealed record Money
{
    public readonly decimal Amount;
    public readonly Currency Currency;

    internal Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money FromDecimal(decimal amount, Currency currency) => new(amount, currency);

    public Money Add(Money summand)
    {
        if (Currency != summand.Currency)
        {
            throw new CurrencyMismatchException("Cannot sum amounts with different currencies");
        }

        return new Money(Amount + summand.Amount, Currency);
    }

    public Money Subtract(Money subtrahend)
    {
        if (Currency != subtrahend.Currency)
        {
            throw new CurrencyMismatchException("Cannot sum amounts with different currencies");
        }

        return new Money(Amount - subtrahend.Amount, Currency);
    }

    public static Money operator +(Money firstSummand, Money secondSummand) => firstSummand.Add(secondSummand);

    public static Money operator -(Money minuend, Money subtrahend) => minuend.Subtract(subtrahend);

    public override string ToString() => $"{Currency} {Amount}";
}
