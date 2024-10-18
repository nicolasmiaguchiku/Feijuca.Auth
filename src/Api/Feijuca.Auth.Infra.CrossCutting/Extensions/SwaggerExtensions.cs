using Feijuca.Auth.Infra.CrossCutting.Filters;
using Feijuca.Auth.Models;
using Flurl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Feijuca.Auth.Infra.CrossCutting.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, AuthSettings authSettings)
        {
            if (authSettings is not null)
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Feijuca.Auth.Api", Version = "v1" });

                    var defaultRealmTokenGeneration = authSettings.Realms.Where(x => x.UseAsDefaultSwaggerTokenGeneration);

                    if (defaultRealmTokenGeneration.Any())
                    {
                        c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                        {
                            Type = SecuritySchemeType.OAuth2,
                            Flows = new OpenApiOAuthFlows
                            {
                                Password = new OpenApiOAuthFlow
                                {
                                    TokenUrl = new Uri(defaultRealmTokenGeneration.First().Issuer.AppendPathSegment($"/protocol/openid-connect/token")),
                                    Scopes = authSettings.Scopes!.Distinct().ToDictionary(key => key, value => value)
                                }
                            }
                        });
                    }

                    c.OperationFilter<AuthorizeCheckOperationFilter>();
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Feijuca.Auth.Api.xml"));
                });
            }

            services.AddSwaggerGen();
            return services;
        }
    }
}
