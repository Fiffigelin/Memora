using Backend.Data;
using Backend.Models.Entities;
using Backend.Repositories;
using Backend.Repositories.Vocabularies;
using Backend.Services;
using Backend.Services.Auth;
using Microsoft.AspNetCore.Identity;
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
        builder.Services.AddScoped<IVocabularyListRepository, VocabularyListRepository>();
        builder.Services.AddScoped<IVocabularyRepository, VocabularyRepository>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        // Services
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<AuthService>();
        builder.Services.AddScoped<TokenService>();
        builder.Services.AddScoped<VocabularyListService>();
        builder.Services.AddScoped<VocabularyService>();

    }
}
