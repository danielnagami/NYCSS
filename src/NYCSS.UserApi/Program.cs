using NYCSS.Infra.SqlServer.Extensions;
using NYCSS.UserApi.Configurations;
using NYCSS.Utils.Extensions;
using NYCSS.Utils.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json", true, true);
builder.Configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);
builder.Configuration.AddEnvironmentVariables();

var configuration = builder.Configuration;
var services = builder.Services;

services.AddJwtConfiguration(configuration);
services.AddApiConfiguration();
services.AddUserDbContextConfiguration(configuration);
services.RegisterServices();
services.AddSwaggerConfiguration();
services.AddMessageBusConfig(configuration);

var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseApiConfiguration(app.Environment);

app.Run();