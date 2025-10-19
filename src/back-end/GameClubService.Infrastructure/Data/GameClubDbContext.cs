using GameClubService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameClubService.Infrastructure.Data;

public class GameClubDbContext(DbContextOptions<GameClubDbContext> options) : DbContext(options)
{
    public DbSet<Club> Clubs => Set<Club>();
    public DbSet<Event> Events => Set<Event>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}



