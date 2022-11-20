using Sovcombank.FinancialTrading.Application.Interfaces;

namespace Sovcombank.FinancialTrading.WebApi;

internal static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
