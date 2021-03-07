using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(RollingRetention.IdentityServer.Areas.Identity.IdentityHostingStartup))]
namespace RollingRetention.IdentityServer.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}