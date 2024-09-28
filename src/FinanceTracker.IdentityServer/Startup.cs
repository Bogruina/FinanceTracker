using FinanceTracker.IdentityServer.Interfaces;

namespace FinanceTracker.IdentityServer;

public static class Startup
{
    public static void ConfigureServices(
        this WebApplicationBuilder buidler,
        IStartupLogger logger
    )
    {
        buidler
            .Services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddInMemoryApiScopes(Config.Config.ApiScopes)
            .AddInMemoryClients(Config.Config.Clients);
    }

    public static void Configure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseIdentityServer();
    }
}
