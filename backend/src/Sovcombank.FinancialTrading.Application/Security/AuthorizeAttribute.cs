namespace Sovcombank.FinancialTrading.Application.Security;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class AuthorizeAttribute : Attribute
{
    public string Roles { get; set; } = "";

    public string Policy { get; set; } = "";
}
