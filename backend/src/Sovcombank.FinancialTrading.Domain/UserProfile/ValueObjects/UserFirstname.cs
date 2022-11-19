using Sovcombank.FinancialTrading.Domain.SimpleTypes;

namespace Sovcombank.FinancialTrading.Domain.UserProfile.ValueObjects;

public sealed record UserFirstname
{
    public readonly String50 Value;

    private UserFirstname(String50 value) => Value = value;

    public static UserFirstname FromString(string firstname)
    {
        firstname = firstname.Trim();

        if (firstname.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(firstname), "User firstname cannot be empty");

        return new UserFirstname(String50.Create(firstname));
    }

    public static implicit operator string(UserFirstname firstname) => firstname.Value;
}
