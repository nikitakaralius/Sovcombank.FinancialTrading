namespace Sovcombank.FinancialTrading.Domain.UserProfile.ValueObjects;

public sealed record UserId
{
    public readonly Guid Value;

    private UserId(Guid value) => Value = value;

    public static UserId FromGuid(Guid id)
    {
        if (id == default)
            throw new ArgumentException("User id cannot be empty", nameof(id));

        return new UserId(id);
    }

    public static implicit operator Guid(UserId id) => id.Value;

    public override string ToString() => Value.ToString();
}
