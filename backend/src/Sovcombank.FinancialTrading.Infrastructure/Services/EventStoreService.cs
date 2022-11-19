using Microsoft.Extensions.Hosting;

namespace Sovcombank.FinancialTrading.Infrastructure.Services;

internal sealed class EventStoreService : IHostedService
{
    private readonly IEventStoreConnection _connection;

    public EventStoreService(IEventStoreConnection connection)
    {
        _connection = connection;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _connection.ConnectAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _connection.Close();
        return Task.CompletedTask;
    }
}
