using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NYCSS.Infra.SqlServer.Data;
using NYCSS.Utils.Identity;

namespace NYCSS.Infra.SqlServer.Extensions
{
    public static class IdentityDI
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(configuration["SqlServer:ConnectionString"], x => x.MigrationsAssembly("NYCSS.AuthApi")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddJwtConfiguration(configuration);

            return services;
        }
    }
}