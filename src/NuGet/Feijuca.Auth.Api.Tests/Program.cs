using Coderaw.Settings.Transformers;
using Feijuca.Auth.Api.Tests.Models;
using Feijuca.Auth.Extensions;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

var applicationSettings = builder.Configuration.GetSection("Settings").Get<Settings>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddApiAuthentication(applicationSettings!.Realms!)
    .AddEndpointsApiExplorer()
    .AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection()
   .UseAuthorization()
   .UseTenantMiddleware();

app.MapControllers();

await app.RunAsync();
