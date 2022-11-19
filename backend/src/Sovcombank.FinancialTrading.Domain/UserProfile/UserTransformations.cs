using Sovcombank.FinancialTrading.Domain.Account;
using Sovcombank.FinancialTrading.Domain.Account.ValueObjects;
using Sovcombank.FinancialTrading.Domain.Common;
using Sovcombank.FinancialTrading.Domain.UserProfile.ValueObjects;

namespace Sovcombank.FinancialTrading.Domain.UserProfile;

public static class UserTransformations
{
    public static UserProfile Apply(UserProfile profile, UserEvent @event)
    {
        Func<UserProfile> transformation = @event switch
        {
            UserEvent.UserBanned banned => () =>
            {
                if (profile is UserProfile.Verified verified)
                    return Apply(verified, banned);

                throw new ArgumentException("Cannot ban not verified user");
            },
            UserEvent.UserRegistered registered => () =>
            {
                return Create(registered);
            },
            UserEvent.UserVerificationRejected rejected => () =>
            {
                if (profile is UserProfile.Unverified unverified)
                    return Apply(unverified, rejected);

                throw new ArgumentException("Cannot reject not unverified user");
            },
            UserEvent.UserVerified => () =>
            {
                if (profile is UserProfile.Unverified unverified)
                    return Apply(unverified);

                throw new ArgumentException("Cannot verify not unverified user");
            },
            _ => throw new ArgumentOutOfRangeException(nameof(@event))
        };

        return transformation();
    }

    public static UserProfile.Unverified Create(UserEvent.UserRegistered @event)
    {
        var id = UserId.FromGuid(@event.Id);
        var email = EmailAddress.FromString(@event.Email);
        var phoneNumber = PhoneNumber.FromString(@event.PhoneNumber);

        return new(id, email, phoneNumber);
    }

    private static UserProfile.Verified Apply(UserProfile.Unverified profile)
    {
        var rubleCurrency = Currency.Ruble;

        var money = new Money(0, rubleCurrency);
        var account = new CurrencyAccount(money);

        Dictionary<Currency, CurrencyAccount> accounts = new()
        {
            {rubleCurrency, account}
        };

        return new(profile.Id, profile.Email, profile.PhoneNumber, accounts);
    }

    private static UserProfile.Banned Apply(UserProfile.Verified profile, UserEvent.UserBanned @event) =>
        new(profile.Id, UserId.FromGuid(@event.BannedBy), @event.Reason);

    private static UserProfile.Rejected Apply(
        UserProfile.Unverified profile,
        UserEvent.UserVerificationRejected @event) =>
        new(profile.Id, UserId.FromGuid(@event.RejectedBy), @event.Reason);
}
