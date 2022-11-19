namespace Sovcombank.FinancialTrading.Domain.UserProfile;

public static class UserEvents
{
    public class UserRegistered
    {
        public Guid Id { get; init; }

        public string Firstname { get; init; } = "";

        public string Lastname { get; init; } = "";

        public string? Patronymic { get; init; }

        public string Email { get; init; } = "";

        public Passport Passport { get; init; } = null!;
    }

    public class UserVerified
    {
        public Guid Id { get; init; }

        public Guid VerifiedBy { get; init; }
    }

    public class UserVerificationRejected
    {
        public Guid Id { get; init; }

        public Guid RejectedBy { get; init; }

        public string Reason { get; init; } = "";
    }

    public class UserBanned
    {
        public Guid Id { get; init; }

        public Guid BannedBy { get; init; }

        public string Reason { get; init; } = "";
    }
}
