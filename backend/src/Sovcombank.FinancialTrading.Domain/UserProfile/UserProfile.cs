using Sovcombank.FinancialTrading.Domain.Common;
using Sovcombank.FinancialTrading.Domain.UserProfile.ValueObjects;

namespace Sovcombank.FinancialTrading.Domain.UserProfile;

public abstract record UserProfile
{
    public record Unverified(UserId Id, FullUserName Username, EmailAddress Email, Passport Passport) : UserProfile;

    public record Verified(UserId Id, FullUserName Username, EmailAddress Email, Passport Passport) : UserProfile;

    public record Banned(UserId Id, UserId BannedBy) : UserProfile;

    public record Administrator(UserId Id, EmailAddress Email) : UserProfile;
}
