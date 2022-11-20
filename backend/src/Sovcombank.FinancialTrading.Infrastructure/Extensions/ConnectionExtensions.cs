using Sovcombank.FinancialTrading.Domain.EventSourcing;

namespace Sovcombank.FinancialTrading.Infrastructure.Extensions;

internal static class ConnectionExtensions
{
    public static Task AppendEventsAsync(
        this IEventStoreConnection connection,
        string streamName, long version, IEvent[] events)
    {
        if (events.Length == 0) return Task.CompletedTask;
        var changes = events.Select(e => e.Serialize()).ToArray();
        return connection.AppendToStreamAsync(streamName, version, changes);
    }
}
