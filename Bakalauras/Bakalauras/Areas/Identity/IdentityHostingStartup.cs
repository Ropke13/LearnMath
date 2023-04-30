using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Bakalauras.Areas.Identity.IdentityHostingStartup))]
namespace Bakalauras.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}