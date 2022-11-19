namespace Sovcombank.FinancialTrading.Domain.UserProfile;

public abstract class UserEvent
{
    public class UserRegistered : UserEvent
    {
        public Guid Id { get; init; }

        public string Email { get; init; } = "";

        public string PhoneNumber { get; init; } = "";
    }

    public class UserVerified : UserEvent
    {
        public Guid Id { get; init; }

        public Guid VerifiedBy { get; init; }
    }

    public class UserVerificationRejected : UserEvent
    {
        public Guid Id { get; init; }

        public Guid RejectedBy { get; init; }

        public string Reason { get; init; } = "";
    }

    public class UserBanned : UserEvent
    {
        public Guid Id { get; init; }

        public Guid BannedBy { get; init; }

        public string Reason { get; init; } = "";
    }

    public class UserAccountReplenished : UserEvent
    {
        public Guid Id { get; init; }

        public int CurrencyCode { get; init; }

        public decimal Replenishment { get; init; }
    }

    public class UserAccountWithdrew : UserEvent
    {
        public Guid Id { get; init; }

        public int CurrencyCode { get; init; }

        public decimal Withdraw { get; init; }
    }

    public class UserCreatedNewAccount : UserEvent
    {
        public Guid Id { get; init; }

        public int CurrencyCode { get; init; }
    }
}
