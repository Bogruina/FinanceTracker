using FinanceTracker.Host.Configs;
using FinanceTracker.Host.Configs.Common;
using FinanceTracker.Host.Interfaces;
using FinanceTracker.Host.Middlewares;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace FinanceTracker.Host;

/// <summary>
/// Application launch configuration.
/// </summary>
public static class Startup
{
    public static void ConfigureServices(
        this WebApplicationBuilder builder,
        IStartupLogger logger
    )
    {
        var configuration = builder.Configuration;

        builder.Services.AddAuthConfig();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerConfig(configuration);
    }

    public static void Configure(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        app.UseExceptionHandler(x =>
            x.UseCustomExceptionHandler(app.Environment)
        );
        app.UseHttpLogging();
        app.UseMiddleware<RequestLoggerMiddleware>();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Finance Tracker API V1");
            options.DocExpansion(DocExpansion.List);
            options.DefaultModelExpandDepth(1);

            options.OAuthClientId("client_id_swagger");
        });
    }
}
