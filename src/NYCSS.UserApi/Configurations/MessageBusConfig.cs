using NYCSS.UserApi.Services;
using NYCSS.Utils.Extensions;

namespace NYCSS.UserApi.Configurations
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration["RabbitMQ:ConnectionString"])
                .AddHostedService<RegisterUserIntegrationHandler>();
        }
    }
}