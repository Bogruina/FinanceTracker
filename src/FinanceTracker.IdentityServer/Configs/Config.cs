using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace FinanceTracker.IdentityServer.Config;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[] { new IdentityResources.OpenId() };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope> { new ApiScope("api1", "Finance Tracker") };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "client_id_swagger",
                ClientSecrets =
                {
                    new Secret("client_secret_swagger".ToSha256()),
                },
                AllowedCorsOrigins = { "http://localhost:5208" },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes =
                {
                    "api1",
                    IdentityServerConstants.StandardScopes.OpenId,
                },
            },
            new Client
            {
                ClientId = "client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,

                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedScopes = { "api1" },
            },
        };
}
