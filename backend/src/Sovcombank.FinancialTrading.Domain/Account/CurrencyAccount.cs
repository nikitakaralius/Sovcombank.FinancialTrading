using Sovcombank.FinancialTrading.Domain.Account.ValueObjects;

namespace Sovcombank.FinancialTrading.Domain.Account;

public interface IReadOnlyCurrencyAccount
{
    Currency Currency { get; }

    decimal Amount { get; }
}

public record CurrencyAccount : IReadOnlyCurrencyAccount
{
    private Money _balance;

    internal CurrencyAccount(Currency currency) =>
        _balance = new Money(0, currency);

    public Currency Currency => _balance.Currency;

    public decimal Amount => _balance.Amount;

    public void Replenish(Money money)
    {
        if (money.Amount < 0)
        {
            throw new InvalidOperationException("Cannot add negative amount of money");
        }

        _balance += money;
    }

    public void Withdraw(Money money)
    {
        if (money.Amount < 0)
        {
            throw new InvalidOperationException("Cannot withdraw negative amount of money");
        }

        var difference = _balance - money;

        if (difference.Amount < 0)
        {
            throw new InvalidOperationException(
                "Cannot withdraw money when there are not enough balance in the account");
        }

        _balance = difference;
    }
}
