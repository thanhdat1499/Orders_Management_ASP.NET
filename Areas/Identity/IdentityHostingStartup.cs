using System;
using DemoIndentityCore.Areas.Identity.Data;
using DemoIndentityCore.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(DemoIndentityCore.Areas.Identity.IdentityHostingStartup))]
namespace DemoIndentityCore.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<DemoIndentityCoreContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DemoIndentityCoreContextConnection")));

                services.AddDefaultIdentity<DemoIndentityCoreUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<DemoIndentityCoreContext>();
            });
        }
    }
}