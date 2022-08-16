using NYCSS.Infra.JsonReader.Interfaces;
using NYCSS.Infra.JsonReader.Services;
using NYCSS.Infra.MongoDB.Interfaces;
using NYCSS.Infra.MongoDB.Services;

namespace NYCSS.SubwayApi.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IMongoService, MongoService>();

            return services;
        }
    }
}