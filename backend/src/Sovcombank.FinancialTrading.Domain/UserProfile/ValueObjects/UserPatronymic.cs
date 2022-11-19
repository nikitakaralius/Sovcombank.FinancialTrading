using Sovcombank.FinancialTrading.Domain.SimpleTypes;

namespace Sovcombank.FinancialTrading.Domain.UserProfile.ValueObjects;

public sealed record UserPatronymic
{
    public readonly String50 Value;

    private UserPatronymic() : this(String50.Create("")) { }

    private UserPatronymic(String50 value) => Value = value;

    public static UserPatronymic FromString(string? patronymic)
    {
        return string.IsNullOrEmpty(patronymic)
            ? None
            : new UserPatronymic(String50.Create(patronymic));
    }

    public static readonly UserPatronymic None = new();

    public static implicit operator string(UserPatronymic patronymic) => patronymic.Value;
}
