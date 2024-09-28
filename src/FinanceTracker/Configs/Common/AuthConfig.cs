namespace FinanceTracker.Host.Configs.Common;

public static class AuthConfig
{
    private const string DefaultAuthNSchema = "Bearer";

    public static void AddAuthConfig(this IServiceCollection services)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = DefaultAuthNSchema;
                options.DefaultChallengeScheme = DefaultAuthNSchema;
                options.DefaultAuthenticateScheme = DefaultAuthNSchema;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = "http://localhost:27924";
                options.TokenValidationParameters.ValidateAudience = false;
                options.RequireHttpsMetadata = false;
            });

        services.AddAuthorization();
    }
}
