using FinanceTracker.IdentityServer.Extensions;
using Microsoft.OpenApi.Models;

namespace FinanceTracker.Host.Configs;

public static class SwaggerConfig
{
    public static void AddSwaggerConfig(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Version = "v1",
                    Title = "FinancreTracker.WebApi",
                    Description = "Finance Tracker Web Api",
                }
            );

            options.AddSecurityDefinition(
                "oauth2",
                new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        ClientCredentials = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri(
                                "http://localhost:27924/connect/token"
                            ),
                            AuthorizationUrl = new Uri(
                                configuration.GetParam("AuthorizationUrl")
                            ),
                            Scopes = new Dictionary<string, string>
                            {
                                { "api1", "Finance Tracker" },
                            },
                        },
                    },
                }
            );
        });
    }
}
