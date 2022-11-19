namespace Sovcombank.FinancialTrading.Domain.Account.Exceptions;

public sealed class CurrencyMismatchException : Exception
{
    public CurrencyMismatchException(string? message) : base(message) { }
}
