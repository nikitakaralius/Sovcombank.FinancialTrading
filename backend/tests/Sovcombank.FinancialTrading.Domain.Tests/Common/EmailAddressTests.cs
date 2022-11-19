using Sovcombank.FinancialTrading.Domain.Common;

namespace Sovcombank.FinancialTrading.Domain.Tests.Common;

[TestFixture]
public class EmailAddressTests
{
    private static readonly string[] InvalidEmails =
    {
        "email", "email.com", "email@", ".email@gmail.com", "email@gmail", "email@gmail.com."
    };

    [Test]
    [TestCaseSource(nameof(InvalidEmails))]
    public void ThrowsWhenInvalidEmail(string invalidEmail)
    {
        Action creation = () => EmailAddress.FromString(invalidEmail);
        creation.Should().Throw<ArgumentException>();
    }
}
