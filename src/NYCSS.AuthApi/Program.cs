using NYCSS.AuthApi.Configurations;
using NYCSS.Infra.SqlServer.Extensions;
using NYCSS.Utils.Extensions;
using NYCSS.Utils.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json", true, true);
builder.Configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);
builder.Configuration.AddEnvironmentVariables();

var configuration = builder.Configuration;

var services = builder.Services;

var jwtConfig = new JwtConfigurationAppSettings();

builder.Configuration.GetSection(JwtConfigurationAppSettings.KEY).Bind(jwtConfig);

services.AddSingleton<JwtConfigurationAppSettings>(jwtConfig);

services.AddIdentityConfiguration(configuration);
services.AddSwaggerConfiguration(configuration);
services.AddMessageBus(configuration["MessageBus:ConnectionString"]);

var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseApiConfiguration(app.Environment);

app.Run();