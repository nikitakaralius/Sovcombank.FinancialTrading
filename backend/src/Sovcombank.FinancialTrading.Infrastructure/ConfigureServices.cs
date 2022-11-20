using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sovcombank.FinancialTrading.Application.Interfaces;
using Sovcombank.FinancialTrading.Infrastructure.EntityFramework;
using Sovcombank.FinancialTrading.Infrastructure.EventStore;
using Sovcombank.FinancialTrading.Infrastructure.Identity;

namespace Sovcombank.FinancialTrading.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        Action<InfrastructureOptions> configure)
    {
        InfrastructureOptions options = new();
        configure(options);

        var esConnection = EventStoreConnection.Create(
            options.EsConnectionString,
            ConnectionSettings.Create().KeepReconnecting(),
            options.EsConnectionName);

        services.AddSingleton(esConnection);
        services.AddHostedService<EventStoreService>();

        services.AddDbContext<ApplicationDbContext>(o =>
        {
            o.UseNpgsql(options.PostgresConnectionString,
                        builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        });

        services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        services.AddAuthentication()
                .AddIdentityServerJwt();

        services.AddSingleton<IUserProfileStore, EsUserProfileStore>();
        services.AddTransient<IIdentityService, IdentityService>();

        return services;
    }
}

public class InfrastructureOptions
{
    public string EsConnectionString { get; set; } = "";

    public string EsConnectionName { get; set; } = "";

    public string PostgresConnectionString { get; set; } = "";
}
