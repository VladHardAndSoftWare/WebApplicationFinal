using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplicationFinal.Areas.Identity.Data;
using WebApplicationFinal.Data;

[assembly: HostingStartup(typeof(WebApplicationFinal.Areas.Identity.IdentityHostingStartup))]
namespace WebApplicationFinal.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<WebApplicationFinalContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("WebApplicationFinalContextConnection")));

                services.AddDefaultIdentity<WebApplicationFinalUser>()
                    .AddEntityFrameworkStores<WebApplicationFinalContext>();
            });
        }
    }
}