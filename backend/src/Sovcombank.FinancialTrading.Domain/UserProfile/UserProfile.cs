using Sovcombank.FinancialTrading.Domain.Account;
using Sovcombank.FinancialTrading.Domain.Account.ValueObjects;
using Sovcombank.FinancialTrading.Domain.Common;
using Sovcombank.FinancialTrading.Domain.EventSourcing;
using Sovcombank.FinancialTrading.Domain.UserProfile.ValueObjects;
using static Sovcombank.FinancialTrading.Domain.UserProfile.UserEvent;

namespace Sovcombank.FinancialTrading.Domain.UserProfile;

public sealed class UserProfile : AggregateRoot
{
    public enum UserState
    {
        PendingVerification = 1,
        Verified = 2,
        Rejected = 3,
        Banned = 4
    }

    private readonly List<CurrencyAccount> _accounts = new();

    private UserProfile() { }

    public UserId Id { get; private set; } = null!;

    public EmailAddress EmailAddress { get; private set; } = null!;

    public PhoneNumber PhoneNumber { get; private set; } = null!;

    public UserId ReviewedBy { get; private set; } = UserId.None;

    public UserState State { get; private set; }

    public IEnumerable<IReadOnlyCurrencyAccount> Accounts => _accounts;

    public UserProfile(UserId id, EmailAddress email, PhoneNumber phoneNumber) =>
        Apply(new UserRegistered
        {
            Id = id,
            Email = email,
            PhoneNumber = phoneNumber
        });

    public void Verify(UserId verifiedBy) =>
        Apply(new UserVerified
        {
            Id = Id,
            VerifiedBy = verifiedBy
        });

    public void Reject(UserId rejectedBy, string reason) =>
        Apply(new UserVerificationRejected
        {
            Id = Id,
            RejectedBy = rejectedBy,
            Reason = reason
        });

    public void Ban(UserId bannedBy, string reason) =>
        Apply(new UserBanned
        {
            Id = Id,
            BannedBy = bannedBy,
            Reason = reason
        });

    public void OpenCurrencyAccount(Currency currency)
    {
        if (AccountExists(currency))
        {
            throw new InvalidOperationException("Cannot open currency account that already exists");
        }

        Apply(new UserOpenedCurrencyAccount
        {
            Id = Id,
            CurrencyCode = currency.NumericCode
        });
    }

    public void Replenish(decimal replenishment, Currency currency)
    {
        if (AccountExists(currency) == false)
        {
            throw new InvalidOperationException("Cannot replenish currency account that do not exist");
        }

        Apply(new UserAccountReplenished
        {
            Id = Id,
            CurrencyCode = currency.NumericCode,
            Replenishment = replenishment
        });
    }

    public void Withdraw(decimal withdrawal, Currency currency)
    {
        if (AccountExists(currency) == false)
        {
            throw new InvalidOperationException("Cannot withdraw currency account that do not exist");
        }

        Apply(new UserAccountWithdrew
        {
            Id = Id,
            CurrencyCode = currency.NumericCode,
            Withdrawal = withdrawal
        });
    }

    protected override void When(IEvent eventHappened)
    {
        var @event = eventHappened as IEvent<UserProfile>;

        Action when = @event switch
        {
            UserRegistered e => () =>
            {
                Id = UserId.FromGuid(e.Id);
                EmailAddress = EmailAddress.FromString(e.Email);
                PhoneNumber = PhoneNumber.FromString(e.PhoneNumber);
                State = UserState.PendingVerification;
            },
            UserVerified e => () =>
            {
                ReviewedBy = UserId.FromGuid(e.VerifiedBy);
                var account = new CurrencyAccount(Currency.Ruble);
                _accounts.Add(account);
                State = UserState.Verified;
            },
            UserVerificationRejected e => () =>
            {
                ReviewedBy = UserId.FromGuid(e.RejectedBy);
                State = UserState.Rejected;
            },
            UserBanned e => () =>
            {
                ReviewedBy = UserId.FromGuid(e.BannedBy);
                State = UserState.Banned;
            },
            UserOpenedCurrencyAccount e => () =>
            {
                var currency = new Currency {NumericCode = e.CurrencyCode};
                var account = new CurrencyAccount(currency);
                _accounts.Add(account);
            },
            UserAccountReplenished e => () =>
            {
                ApplyCurrencyOperation(e.Replenishment, e.CurrencyCode, (account, money) => account.Replenish(money));
            },
            UserAccountWithdrew e => () =>
            {
                ApplyCurrencyOperation(e.Withdrawal, e.CurrencyCode, (account, money) => account.Withdraw(money));
            },
            _ => throw new ArgumentOutOfRangeException(nameof(@event))
        };

        when();
    }

    protected override void EnsureValidState() { }

    private bool AccountExists(Currency currency) =>
        _accounts.Any(account => account.Currency == currency);

    private void ApplyCurrencyOperation(decimal amount, int currencyCode, Action<CurrencyAccount, Money> action)
    {
        var currency = new Currency {NumericCode = currencyCode};
        var money = new Money(amount, currency);
        var account = FindAccount(currency)!;
        action(account, money);
    }

    private CurrencyAccount? FindAccount(Currency currency) =>
        _accounts.Find(x => x.Currency == currency);
}
