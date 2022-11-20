using Sovcombank.FinancialTrading.Domain.UserProfile;
using Sovcombank.FinancialTrading.Domain.UserProfile.ValueObjects;

namespace Sovcombank.FinancialTrading.Application.Interfaces;

public interface IUserProfileStore
{
    Task<bool> ExistsAsync(UserId userId);

    Task SaveAsync(UserProfile userProfile);

    Task<UserProfile> LoadAsync(UserId userId);
}
