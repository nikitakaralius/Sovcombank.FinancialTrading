using Sovcombank.FinancialTrading.Domain.SimpleTypes;

namespace Sovcombank.FinancialTrading.Domain.UserProfile.ValueObjects;

public sealed record UserPatronymic
{
    public readonly String50 Value;

    private UserPatronymic() : this(String50.Create("")) { }

    private UserPatronymic(String50 value) => Value = value;

    public UserPatronymic FromString(string patronymic)
    {
        patronymic = patronymic.Trim();

        return patronymic.Length == 0
            ? None
            : new UserPatronymic(String50.Create(patronymic));
    }

    public static readonly UserPatronymic None = new();

    public static implicit operator string(UserPatronymic patronymic) => patronymic.Value;
}
