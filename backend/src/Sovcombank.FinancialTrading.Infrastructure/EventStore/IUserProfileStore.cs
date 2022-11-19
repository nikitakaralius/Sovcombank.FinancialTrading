namespace Sovcombank.FinancialTrading.Infrastructure.EventStore;

public interface IUserProfileStore
{
    Task<bool> ExistsAsync(UserId userId);

    Task SaveAsync(UserProfile userProfile);

    Task<UserProfile> LoadAsync(UserId userId);
}
