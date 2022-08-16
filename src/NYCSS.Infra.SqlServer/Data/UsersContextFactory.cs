using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NYCSS.Infra.SqlServer.Data
{
    public class UsersContextFactory : IDesignTimeDbContextFactory<UsersContext>
    {
        public UsersContextFactory()
        {

        }

        public UsersContext CreateDbContext(string[] args)
        {
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);

            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
               .AddJsonFile("appsettings.json", false, true)
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<UsersContext>();
            optionsBuilder.UseSqlServer(configuration["SqlServer:ConnectionString"]);

            return new UsersContext(optionsBuilder.Options, null);
        }
    }
}