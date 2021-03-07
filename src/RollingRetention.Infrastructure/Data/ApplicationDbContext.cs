using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using IdentityServer4.EntityFramework.Options;
using RollingRetention.Core.Entities;

namespace RollingRetention.Infrastructure.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; Initial Catalog=RollingRetentionDB; Trusted_Connection=True")
                    .UseLazyLoadingProxies();
            }
        }
    }

    internal class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; Initial Catalog=RollingRetentionDB; Trusted_Connection=True")
                .UseLazyLoadingProxies();

            return new ApplicationDbContext(optionsBuilder.Options, new OptionsWrapper<OperationalStoreOptions>(new OperationalStoreOptions()));
        }
    }
}