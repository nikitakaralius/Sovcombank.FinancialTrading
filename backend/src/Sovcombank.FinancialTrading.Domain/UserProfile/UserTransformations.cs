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

        var fullname = new FullUserName(
            UserFirstname.FromString(@event.Firstname),
            UserLastname.FromString(@event.Lastname),
            UserPatronymic.FromString(@event.Patronymic));

        var email = EmailAddress.FromString(@event.Email);
        var passport = @event.Passport;

        return new(id, fullname, email, passport);
    }

    private static UserProfile.Verified Apply(UserProfile.Unverified profile) =>
        new(profile.Id, profile.Username, profile.Email, profile.Passport);

    private static UserProfile.Banned Apply(UserProfile.Verified profile, UserEvent.UserBanned @event) =>
        new(profile.Id, UserId.FromGuid(@event.BannedBy), @event.Reason);

    private static UserProfile.Rejected Apply(
        UserProfile.Unverified profile,
        UserEvent.UserVerificationRejected @event) =>
        new(profile.Id, UserId.FromGuid(@event.RejectedBy), @event.Reason);
}
