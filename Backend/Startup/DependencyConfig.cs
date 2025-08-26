using Backend.Data;
using Backend.Repositories;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend.Startup;

public static class DependenciesConfig
{
    public static void AddDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<MemoraDbContext>(options =>
            options.UseSqlite(DatabaseConfig.ConnectionString));

        // Repos
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        // Services
        builder.Services.AddScoped<UserService>();
    }
}
