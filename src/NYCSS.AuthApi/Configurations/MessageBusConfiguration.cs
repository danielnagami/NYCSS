using NYCSS.AuthApi.Interfaces;
using NYCSS.AuthApi.Services;

namespace NYCSS.AuthApi.Configurations
{
    public static class MessageBusConfiguration
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException();

            services.AddSingleton<IMessageBus>(new MessageBus(connectionString));

            return services;
        }
    }
}