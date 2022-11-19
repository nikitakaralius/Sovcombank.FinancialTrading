using System.Text.RegularExpressions;

namespace Sovcombank.FinancialTrading.Domain.Common;

public sealed record PhoneNumber
{
    public readonly string Value;

    private PhoneNumber(string value) => Value = value;

    public static readonly Regex PhoneNumberTemplate =
        new("^(\\+7|7|8)?[\\s\\-]?\\(?[489][0-9]{2}\\)?[\\s\\-]?[0-9]{3}[\\s\\-]?[0-9]{2}[\\s\\-]?[0-9]{2}$",
            RegexOptions.Compiled);

    public static PhoneNumber FromString(string phoneNumber)
    {
        if (PhoneNumberTemplate.IsMatch(phoneNumber) == false)
            throw new ArgumentException($"{phoneNumber} is not valid phone number", nameof(phoneNumber));

        return new PhoneNumber(phoneNumber);
    }
}
