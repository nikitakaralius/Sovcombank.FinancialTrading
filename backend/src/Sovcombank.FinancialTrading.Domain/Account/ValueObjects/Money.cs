using Sovcombank.FinancialTrading.Domain.Account.Exceptions;

namespace Sovcombank.FinancialTrading.Domain.Account.ValueObjects;

public sealed record Money
{
    public readonly decimal Amount;
    public readonly Currency Currency;

    private Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money FromDecimal(decimal amount, int currencyCode, ICurrencyLookup currencyLookup)
    {
        var currency = currencyLookup.FindCurrency(currencyCode);

        if (currency.InUse == false)
        {
            throw new ArgumentException($"Currency {currencyCode} is not valid");
        }

        return new Money(amount, currency);
    }

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
