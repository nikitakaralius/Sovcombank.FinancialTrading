using System.Text.RegularExpressions;

namespace Sovcombank.FinancialTrading.Domain.Common;

public record EmailAddress
{
    private readonly string Value;

    private EmailAddress(string value) => Value = value;

    private static readonly Regex EmailTemplate = new(
            "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$",
            RegexOptions.Compiled);

    public static EmailAddress FromString(string emailAddress)
    {
        if (EmailTemplate.IsMatch(emailAddress) == false)
            throw new ArgumentException($"{emailAddress} is not valid email", nameof(emailAddress));

        return new EmailAddress(emailAddress);
    }

    public static implicit operator string(EmailAddress address) => address.Value;
}
