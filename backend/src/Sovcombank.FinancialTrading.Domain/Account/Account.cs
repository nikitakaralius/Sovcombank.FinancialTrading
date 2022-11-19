using Sovcombank.FinancialTrading.Domain.Account.ValueObjects;

namespace Sovcombank.FinancialTrading.Domain.Account;

public sealed class Account : IEquatable<Account>
{
    public readonly Currency Currency;

    public readonly Money Amount;

    public bool Equals(Account? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Currency.Equals(other.Currency);
    }

    public override bool Equals(object? obj) =>
        ReferenceEquals(this, obj) || obj is Account other && Equals(other);

    public override int GetHashCode() =>
        Currency.GetHashCode();
}
