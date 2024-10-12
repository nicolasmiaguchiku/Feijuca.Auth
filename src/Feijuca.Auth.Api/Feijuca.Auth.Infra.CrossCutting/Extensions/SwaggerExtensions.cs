using Feijuca.Auth.Infra.CrossCutting.Filters;
using Feijuca.Auth.Services.Models;
using Flurl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Feijuca.Auth.Infra.CrossCutting.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, AuthSettings keyCloakSettings)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Feijuca.Auth.Api", Version = "v1" });

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri(keyCloakSettings.AuthServerUrl.AppendPathSegment("realms/smartconsig/protocol/openid-connect/token")),
                            Scopes = keyCloakSettings.Scopes!.Distinct().ToDictionary(key => key, value => value)
                        }
                    }
                });

                c.OperationFilter<AuthorizeCheckOperationFilter>();
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Feijuca.Auth.UI.xml"));
            });

            return services;
        }
    }
}
