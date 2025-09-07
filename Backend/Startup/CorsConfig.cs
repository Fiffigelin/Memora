namespace Backend.Startup;

public static class CorsConfig
{
    private const string AllowAllPolicy = "AllowFrontend";
    public static void AddCorsServices(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(AllowAllPolicy, policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
            });
        });
    }

    public static void ApplyCorsConfig(this WebApplication app)
    {
        app.UseCors("AllowFrontend");
    }
}
