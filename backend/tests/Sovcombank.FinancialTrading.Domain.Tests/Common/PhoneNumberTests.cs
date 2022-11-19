using Sovcombank.FinancialTrading.Domain.Common;

namespace Sovcombank.FinancialTrading.Domain.Tests.Common;

[TestFixture]
public class PhoneNumberTests
{
    private static readonly string[] ValidPhoneNumbers =
    {
        "+79855310868",
        "+79855310868",
        "+7 (926) 777-77-77",
        "84456464641",
        "89855310868"
    };

    private static readonly string[] InvalidPhoneNumbers =
    {
        "880084545454",
        "88008454545411",
        "465456465465",
        "784545487878",
        "12125465415646",
        "454658498797894",
        "231321546545",
        "231321321316548",
        "7889213554654",
    };

    [Test]
    [TestCaseSource(nameof(ValidPhoneNumbers))]
    public void CreatesValidPhoneNumber(string phoneNumber)
    {
        Action creation = () => PhoneNumber.FromString(phoneNumber);
        creation.Should().NotThrow();
    }

    [Test]
    [TestCaseSource(nameof(InvalidPhoneNumbers))]
    public void ThrowsWhenInvalidPhoneNumber(string phoneNumber)
    {
        Action creation = () => PhoneNumber.FromString(phoneNumber);
        creation.Should().Throw<ArgumentException>();
    }
}
