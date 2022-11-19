namespace Sovcombank.FinancialTrading.Domain.Account;

public sealed record Currency
{
    internal Currency() { }

    public int NumericCode { get; internal init; }

    public bool InUse { get; private init; } = true;

    public static Currency FromNumericCode(int numericCode, ICurrencyLookup currencyLookup)
    {
        var currency = currencyLookup.FindCurrency(numericCode);

        if (currency.InUse == false)
        {
            throw new InvalidOperationException("Cannot create currency account with the currency that not in use");
        }

        return new Currency {NumericCode = numericCode};
    }

    public static Currency None => new() {InUse = false};

    public static Currency Ruble => new() {NumericCode = 643};
};
