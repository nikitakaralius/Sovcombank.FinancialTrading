namespace Sovcombank.FinancialTrading.Domain.Account;

public sealed record Currency
{
    public int NumericCode { get; init; }

    public bool InUse { get; init; } = true;

    public static Currency None => new() {InUse = false};

    public static Currency Ruble => new() {NumericCode = 643};
};
