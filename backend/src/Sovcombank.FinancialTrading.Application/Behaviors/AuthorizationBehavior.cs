using System.Reflection;
using MediatR;
using Sovcombank.FinancialTrading.Application.Exceptions;
using Sovcombank.FinancialTrading.Application.Interfaces;
using Sovcombank.FinancialTrading.Application.Security;

namespace Sovcombank.FinancialTrading.Application.Behaviors;

internal sealed class AuthorizationBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;

    public AuthorizationBehaviour(ICurrentUserService currentUserService, IIdentityService identityService)
    {
        _currentUserService = currentUserService;
        _identityService = identityService;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>().ToArray();

        if (authorizeAttributes.Length == 0)
        {
            return await next();
        }

        if (_currentUserService.UserId is null)
        {
            throw new UnauthorizedAccessException();
        }

        bool authorized = await IsAuthorizedAsync(authorizeAttributes);

        if (authorized == false)
        {
            throw new ForbiddenAccessException();
        }

        var attributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy)).ToArray();

        if (attributesWithPolicies.Length == 0)
        {
            return await next();
        }

        var polices = attributesWithPolicies.Select(a => a.Policy);

        foreach (string policy in polices)
        {
            authorized = await _identityService.AuthorizeAsync(_currentUserService.UserId, policy);

            if (authorized == false)
            {
                throw new ForbiddenAccessException();
            }
        }

        return await next();
    }

    private async Task<bool> IsAuthorizedAsync(AuthorizeAttribute[] authorizeAttributes)
    {
        AuthorizeAttribute[] attributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles)).ToArray();

        bool authorized = false;

        if (attributesWithRoles.Length <= 0) return authorized;

        var separatedRoles = attributesWithRoles.Select(a => a.Roles.Split(','));

        foreach (string[] roles in separatedRoles)
        {
            foreach (string role in roles)
            {
                bool isInRole = await _identityService.IsIsInRoleAsync(
                    _currentUserService.UserId!, role.Trim());

                if (isInRole)
                {
                    authorized = true;
                    break;
                }
            }
        }

        return authorized;
    }
}
