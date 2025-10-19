using GameClubService.Domain.Interfaces;
using GameClubService.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using GameClubService.Infrastructure.Data;

namespace GameClubService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration?.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string not found in configuration.");

        services.AddDbContext<GameClubDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IClubRepository, ClubRepository>();
    
        return services;
    }
}