using Feijuca.Auth.Infra.CrossCutting.Config;
using Feijuca.Auth.Infra.CrossCutting.Extensions;
using Feijuca.Auth.Infra.CrossCutting.Handlers;
using Feijuca.Auth.Infra.CrossCutting.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Logging
    .ClearProviders()
    .AddFilter("Microsoft", LogLevel.Warning)
    .AddFilter("Microsoft", LogLevel.Critical);

builder.Configuration
    .AddJsonFile("appsettings.json", false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{enviroment}.json", true, reloadOnChange: true)
    .AddEnvironmentVariables();

var applicationSettings = builder.Configuration.GetApplicationSettings(builder.Environment);

builder.Services
    .AddSingleton<ISettings>(applicationSettings)
    .AddControllers();

builder.Services.AddSwaggerGen();
builder.Services
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddProblemDetails()
    .AddApiAuthentication(applicationSettings.AuthSettings)
    .AddLoggingDependency()
    .AddMediator()
    .AddRepositories()
    .AddServices()
    .AddSwagger(applicationSettings.AuthSettings)
    .AddHttpClients(applicationSettings.AuthSettings)
    .AddEndpointsApiExplorer()
    .AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins", policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });

var app = builder.Build();

app.UseCors("AllowAllOrigins")
   .UseExceptionHandler()
   .UseSwagger()
   .UseSwaggerUI(c =>
   {
       c.SwaggerEndpoint("/swagger/v1/swagger.json", "Feijuca.Auth.Api");
       c.OAuthClientId(applicationSettings!.AuthSettings!.ClientId);
       c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
   });

app.UseHttpsRedirection()
   .UseAuthorization()
   .UseMiddleware<TenantMiddleware>();

app.MapControllers();

await app.RunAsync();
