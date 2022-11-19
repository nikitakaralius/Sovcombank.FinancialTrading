namespace Sovcombank.FinancialTrading.Domain.Account;

public sealed record Currency
{
    public int NumericCode { get; init; }

    public string Code { get; init; } = "";

    public int Nominal { get; init; }

    public string Name { get; init; } = "";

    public decimal Value { get; init; }
};
