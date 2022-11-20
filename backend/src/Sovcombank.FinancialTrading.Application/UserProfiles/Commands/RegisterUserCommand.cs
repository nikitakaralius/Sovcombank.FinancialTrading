using Sovcombank.FinancialTrading.Application.Interfaces;
using Sovcombank.FinancialTrading.Domain.Common;
using Sovcombank.FinancialTrading.Domain.UserProfile;
using Sovcombank.FinancialTrading.Domain.UserProfile.ValueObjects;

namespace Sovcombank.FinancialTrading.Application.UserProfiles.Commands;

public sealed class RegisterUserCommand : IRequest<Guid>
{
    public string EmailAddress { get; init; } = "";

    public string PhoneNumber { get; init; } = "";

    public string Password { get; init; } = "";
}

internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUserProfileStore _store;
    private readonly IIdentityService _identityService;

    public RegisterUserCommandHandler(IUserProfileStore store, IIdentityService identityService)
    {
        _store = store;
        _identityService = identityService;
    }

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        (var result, string? userId) =
            await _identityService.CreateUserAsync(request.EmailAddress, request.PhoneNumber, request.Password);

        if (result.Succeeded == false)
        {
            throw new InvalidOperationException($"Unable to create a new user. {string.Join(" ", result.Errors)}");
        }

        var profile = new UserProfile(
            UserId.FromGuid(Guid.Parse(userId)),
            EmailAddress.FromString(request.EmailAddress),
            PhoneNumber.FromString(request.PhoneNumber));

        await _store.SaveAsync(profile);

        return profile.Id;
    }
}
