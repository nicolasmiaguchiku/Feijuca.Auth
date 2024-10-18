using Feijuca.Auth.Infra.CrossCutting.Config;
using Feijuca.Auth.Infra.CrossCutting.Extensions;
using Feijuca.Auth.Infra.CrossCutting.Handlers;
using Feijuca.Auth.Infra.CrossCutting.Middlewares;
using Feijuca.Auth.Models;

var builder = WebApplication.CreateBuilder(args);
var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Configuration
    .AddJsonFile("appsettings.json", false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{enviroment}.json", true, reloadOnChange: true)
    .AddEnvironmentVariables();

var applicationSettings = builder.Configuration.GetSection("MongoSettings").Get<MongoSettings>()!;

builder.Services
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddProblemDetails()
    .AddMediator()
    .AddRepositories()
    .AddServices()
    .AddMongo(applicationSettings)
    .AddApiAuthentication(out AuthSettings authSettings)
    .AddEndpointsApiExplorer()
    .AddSwagger(authSettings)
    .AddHttpClients(authSettings?.AuthServerUrl)
    .AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins", policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    })
    .AddControllers();

var app = builder.Build();

app.UseCors("AllowAllOrigins")
   .UseExceptionHandler()
   .UseSwagger()
   .UseSwaggerUI(c =>
   {
       c.SwaggerEndpoint("/swagger/v1/swagger.json", "Feijuca.Auth.Api");
       c.OAuthClientId(authSettings?.ClientSecret ?? "");
       c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
   });

if (authSettings is not null)
{
    app.UseAuthorization()
       .UseMiddleware<TenantMiddleware>();
}

app.UseHttpsRedirection()
   .UseHttpsRedirection()
   .UseMiddleware<ConfigValidationMiddleware>();

app.MapControllers();

await app.RunAsync();
