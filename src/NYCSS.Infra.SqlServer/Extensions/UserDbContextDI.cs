using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NYCSS.Infra.SqlServer.Data;
using NYCSS.Infra.SqlServer.Interfaces;

namespace NYCSS.Infra.SqlServer.Extensions
{
    public static class UserDbContextDI
    {
        public static IServiceCollection AddUserDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UsersContext>(options =>
            options.UseSqlServer(configuration["SqlServer:ConnectionString"], x => x.MigrationsAssembly("NYCSS.UserAPI")));

            return services;
        }
    }
}