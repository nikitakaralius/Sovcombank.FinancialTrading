using Sovcombank.FinancialTrading.Domain.SimpleTypes;

namespace Sovcombank.FinancialTrading.Domain.UserProfile.ValueObjects;

public sealed record UserLastname
{
    public readonly String50 Value;

    private UserLastname(String50 value) => Value = value;

    public UserLastname FromString(string lastname)
    {
        lastname = lastname.Trim();

        if (lastname.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(lastname), "User lastname cannot be empty");

        return new UserLastname(String50.Create(lastname));
    }

    public static implicit operator string(UserLastname lastname) => lastname.Value;
}
