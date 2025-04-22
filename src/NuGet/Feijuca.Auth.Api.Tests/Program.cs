using Coderaw.Settings.Transformers;
using Feijuca.Auth.Api.Tests.Extensions;
using Feijuca.Auth.Api.Tests.Models;
using Feijuca.Auth.Http.Client;
using Feijuca.Auth.Middlewares.TenantMiddleware;
using Scalar.AspNetCore;


var httpClientFeijuca = new HttpClient
{
    BaseAddress = new Uri("https://localhost:7018")
};


httpClientFeijuca.DefaultRequestHeaders.Add("Tenant", "smartconsig");

var feijucaCient = new FeijucaAuthClient(httpClientFeijuca);

var token = await feijucaCient.LoginAsync(CancellationToken.None);

var xx = await feijucaCient.GetUserAsync("felipe.mattioli@coderaw.io", token.Data.AccessToken!, CancellationToken.None);

var lll = 10;



var builder = WebApplication.CreateBuilder(args);

var applicationSettings = builder.Configuration.GetSection("Settings").Get<Settings>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddApiAuthentication(applicationSettings!)
    .AddEndpointsApiExplorer()
    .AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseCors("AllowAllOrigins")
   .UseHttpsRedirection()
   .UseAuthorization()
   .UseMiddleware<TenantMiddleware>();

app.MapControllers();

await app.RunAsync();
