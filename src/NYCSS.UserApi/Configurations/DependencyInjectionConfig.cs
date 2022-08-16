using FluentValidation.Results;

using MediatR;

using NYCSS.Infra.MongoDB.Interfaces;
using NYCSS.Infra.MongoDB.Services;
using NYCSS.Infra.SqlServer.Data;
using NYCSS.Infra.SqlServer.Interfaces;
using NYCSS.UserApi.Application.Commands;
using NYCSS.UserApi.Application.Events;
using NYCSS.Utils.Configurations;
using NYCSS.Utils.Mediator;
using NYCSS.Utils.User;

namespace NYCSS.UserApi.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IMongoService, MongoService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddMediatRDI();

            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<IRequestHandler<RegisterUserCommand, ValidationResult>, UserCommandHandler>();

            services.AddScoped<INotificationHandler<UserRegisteredEvent>, UserEventHandler>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UsersContext>();
        }
    }
}