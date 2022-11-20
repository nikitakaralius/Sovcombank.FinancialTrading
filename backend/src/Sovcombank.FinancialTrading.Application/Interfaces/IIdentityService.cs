using Sovcombank.FinancialTrading.Application.Common;

namespace Sovcombank.FinancialTrading.Application.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsIsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string email, string phoneNumber, string password);

    Task<Result> DeleteUserAsync(string userId);
}
