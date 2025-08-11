using Mattioli.Configurations.Transformers;
using Feijuca.Auth.Api.Tests.Models;
using Feijuca.Auth.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var applicationSettings = builder.Configuration.GetSection("Settings").Get<Settings>();

builder.Services.AddControllers();

builder.Services
    .AddApiAuthentication(applicationSettings!.Realms!)
    .AddEndpointsApiExplorer()
    .AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();

app.MapControllers();

app.UseTenantMiddleware();

await app.RunAsync();
