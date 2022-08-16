using NYCSS.SubwayApi.Configurations;
using NYCSS.Utils.Extensions;
using NYCSS.Utils.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json", true, true);
builder.Configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);
builder.Configuration.AddEnvironmentVariables();

var services = builder.Services;

services.RegisterServices();
services.AddControllers();
services.AddSwaggerConfiguration();
services.AddJwtConfiguration(builder.Configuration);

var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseApiConfiguration(app.Environment);

app.Run();