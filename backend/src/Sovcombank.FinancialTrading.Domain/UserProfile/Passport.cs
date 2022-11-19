namespace Sovcombank.FinancialTrading.Domain.UserProfile;

public sealed class Passport
{
    public int Series { get; init; }

    public int Number { get; init; }

    public DateOnly Birthdate { get; init; }

    public string UnitCode { get; init; } = "";

    public string IssuedBy { get; init; } = "";

    public DateOnly IssueDate { get; init; }
}
