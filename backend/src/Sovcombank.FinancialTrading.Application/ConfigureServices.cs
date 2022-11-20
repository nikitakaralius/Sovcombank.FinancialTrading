using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Sovcombank.FinancialTrading.Application.Behaviors;

namespace Sovcombank.FinancialTrading.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));

        return services;
    }
}
