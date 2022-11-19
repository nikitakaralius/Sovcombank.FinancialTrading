namespace Sovcombank.FinancialTrading.Domain.Account;

public interface ICurrencyLookup
{
    Currency FindCurrency(int numericCode);
}
