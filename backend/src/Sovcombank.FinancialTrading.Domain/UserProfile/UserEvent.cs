namespace Sovcombank.FinancialTrading.Domain.UserProfile;

public abstract class UserEvent
{
    public class UserRegistered : UserEvent
    {
        public Guid Id { get; init; }

        public string Firstname { get; init; } = "";

        public string Lastname { get; init; } = "";

        public string? Patronymic { get; init; }

        public string Email { get; init; } = "";
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
}
