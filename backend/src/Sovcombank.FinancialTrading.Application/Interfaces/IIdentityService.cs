using Sovcombank.FinancialTrading.Application.Common;

namespace Sovcombank.FinancialTrading.Application.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(Guid userId);

    Task<bool> IsIsInRoleAsync(Guid userId, string role);

    Task<bool> AuthorizeAsync(Guid userId, string policyName);

    Task<(Result Result, Guid UserId)> CreateUserAsync(string email, string phoneNumber, string password);

    Task<Result> DeleteUserAsync(Guid userId);
}
