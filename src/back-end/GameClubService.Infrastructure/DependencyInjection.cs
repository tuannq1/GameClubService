using GameClubService.Domain.Interfaces;
using GameClubService.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace GameClubService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration["Svc:ConnectionStrings:GameClubDbCons"]
            ?? throw new InvalidOperationException("Connection string 'Svc:ConnectionStrings:GameClubDbCons' not found in configuration.");

        services.AddDbContext<GameClubDbContext>(opt => 
            opt.UseSqlite(connectionString)
        );

        services.AddScoped<ClubRepository, IClubRepository>();
        
        return services;
    }
}