using Microsoft.Extensions.DependencyInjection;

namespace Sovcombank.FinancialTrading.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        Action<EventStoreOptions> configure)
    {
        EventStoreOptions options = new();
        configure(options);

        var esConnection = EventStoreConnection.Create(
            options.ConnectionString,
            ConnectionSettings.Create().KeepReconnecting(),
            options.ConnectionName);

        services.AddSingleton(esConnection);

        services.AddHostedService<EventStoreService>();

        return services;
    }
}

public class EventStoreOptions
{
    public string ConnectionString { get; set; } = "";

    public string ConnectionName { get; set; } = "";
}
