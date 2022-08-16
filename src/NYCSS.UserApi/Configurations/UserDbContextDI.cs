//using Microsoft.EntityFrameworkCore;

//using NYCSS.UserApi.Data;

//namespace NYCSS.UserApi.Configurations
//{
//    public static class UserDbContextDI
//    {
//        public static IServiceCollection AddUserDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
//        {
//            services.AddDbContext<UsersContext>(options =>
//            options.UseSqlServer(configuration["SqlServer:ConnectionString"], x => x.MigrationsAssembly("NYCSS.UserAPI")));

//            return services;
//        }
//    }
//}