using ELearn.Infrastructure.Entity.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ELearn.Infrastructure.Entity
{
    public class AuthContext: ApiAuthorizationDbContext<ApplicationUser>
    {
        public AuthContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
    }
}