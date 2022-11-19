using Sovcombank.FinancialTrading.Domain.Account;
using Sovcombank.FinancialTrading.Domain.Common;
using Sovcombank.FinancialTrading.Domain.UserProfile.ValueObjects;

namespace Sovcombank.FinancialTrading.Domain.UserProfile;

public abstract record UserProfile
{
    public record Unverified(UserId Id, EmailAddress Email, PhoneNumber PhoneNumber) : UserProfile;

    public record Verified(
        UserId Id,
        EmailAddress Email,
        PhoneNumber PhoneNumber,
        IDictionary<Currency, CurrencyAccount> Accounts) : UserProfile;

    public record Rejected(UserId Id, UserId RejectedBy, string Reason) : UserProfile;

    public record Banned(UserId Id, UserId BannedBy, string Reason) : UserProfile;

    public record Administrator(UserId Id, EmailAddress Email) : UserProfile;
}
