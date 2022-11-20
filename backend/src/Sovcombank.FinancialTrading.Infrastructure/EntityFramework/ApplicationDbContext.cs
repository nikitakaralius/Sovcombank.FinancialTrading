using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sovcombank.FinancialTrading.Infrastructure.Identity;

namespace Sovcombank.FinancialTrading.Infrastructure.EntityFramework;

internal sealed class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) :
        base(options, operationalStoreOptions) { }
}
