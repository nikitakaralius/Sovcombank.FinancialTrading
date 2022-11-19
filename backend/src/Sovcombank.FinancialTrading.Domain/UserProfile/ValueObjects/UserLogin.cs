using Sovcombank.FinancialTrading.Domain.SimpleTypes;

namespace Sovcombank.FinancialTrading.Domain.UserProfile.ValueObjects;

public sealed record UserLogin(String50 Value)
{
    public static implicit operator string(UserLogin login) => login.Value;
}
