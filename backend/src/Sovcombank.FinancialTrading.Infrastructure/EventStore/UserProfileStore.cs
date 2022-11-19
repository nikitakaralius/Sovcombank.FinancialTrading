using Sovcombank.FinancialTrading.Infrastructure.Extensions;

namespace Sovcombank.FinancialTrading.Infrastructure.EventStore;

internal sealed class UserProfileStore : IUserProfileStore
{
    private const int StreamSlice = 1024;
    private readonly IEventStoreConnection _connection;

    public UserProfileStore(IEventStoreConnection connection) => _connection = connection;

    public async Task<bool> ExistsAsync(UserId userId)
    {
        string stream = StreamName(userId);
        var response = await _connection.ReadEventAsync(stream, 1, false);
        return response.Status != EventReadStatus.NoStream;
    }

    public async Task SaveAsync(UserProfile userProfile)
    {
        string stream = StreamName(userProfile);
        await _connection.AppendEventsAsync(stream, userProfile.Version, userProfile.Changes().ToArray());
    }

    public async Task<UserProfile> LoadAsync(UserId userId)
    {
        string stream = StreamName(userId);
        var aggregate = (UserProfile) Activator.CreateInstance(typeof(UserProfile), true)!;
        var page = await _connection.ReadStreamEventsForwardAsync(stream, 0, StreamSlice, false);

        aggregate.Load(page.Events.Select(e => e.Deserialize()).ToArray());

        return aggregate;
    }

    private static string StreamName(UserId userId) => $"{nameof(UserProfile)}-{userId}";

    private static string StreamName(UserProfile profile) => $"{nameof(UserProfile)}-{profile.Id}";
}
