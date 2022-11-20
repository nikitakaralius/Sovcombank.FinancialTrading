using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sovcombank.FinancialTrading.Application.Common;
using Sovcombank.FinancialTrading.Application.Interfaces;

namespace Sovcombank.FinancialTrading.Infrastructure.Identity;

internal sealed class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }

    public async Task<string?> GetUserNameAsync(Guid userId)
    {
        var user = await FindUserAsync(userId);

        return user?.UserName;
    }

    public async Task<(Result Result, Guid UserId)> CreateUserAsync(string email, string phoneNumber, string password)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            PhoneNumber = phoneNumber
        };

        var result = await _userManager.CreateAsync(user, password);

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<bool> IsIsInRoleAsync(Guid userId, string role)
    {
        var user = await FindUserAsync(userId);

        return user is not null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(Guid userId, string policyName)
    {
        var user = await FindUserAsync(userId);

        if (user is null) return false;

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(Guid userId)
    {
        var user = await FindUserAsync(userId);

        return user is not null ? await DeleteUserAsync(user) : Result.Success();
    }

    private async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    private async Task<ApplicationUser?> FindUserAsync(Guid userId) =>
        await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
}
