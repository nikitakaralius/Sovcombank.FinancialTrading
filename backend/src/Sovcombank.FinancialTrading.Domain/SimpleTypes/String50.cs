namespace Sovcombank.FinancialTrading.Domain.SimpleTypes;

public sealed record String50
{
    public readonly string Value;

    private String50(string value) => Value = value;

    public static String50 Create(string value)
    {
        if (value.Length > 50)
            throw new ArgumentException("String length must be less than 50 characters");

        return new String50(value);
    }

    public static implicit operator string(String50 displayName) => displayName.Value;
}
