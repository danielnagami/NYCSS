using Microsoft.Extensions.DependencyInjection;

using NYCSS.Utils.MessageBus;

namespace NYCSS.Utils.Extensions
{
    public static class MessageBusConfiguration
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException();

            services.AddSingleton<IMessageBus>(new MessageBus.MessageBus(connectionString));

            return services;
        }
    }
}