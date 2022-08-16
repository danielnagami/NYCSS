using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace NYCSS.Utils.Configurations
{
    public static class MediatRConfiguration
    {
        public static IServiceCollection AddMediatRDI(this IServiceCollection services)
        {
            services.AddMediatR(typeof(MediatRConfiguration));

            return services;
        }
    }
}