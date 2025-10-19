using GameClubService.Domain.Entities;
using GameClubService.Domain.Interfaces;
using GameClubService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameClubService.Infrastructure.Repositories;

public class ClubRepository : IClubRepository
{
    private readonly GameClubDbContext _db;

    public ClubRepository(GameClubDbContext db) => _db = db;

    public async Task<Guid?> CreateClubAsync(string name, string description)
    {
        if (await _db.Clubs.AnyAsync(c => c.Name == name))
            return null;

        var club = new Club(name, description);
        _db.Clubs.Add(club);
        await _db.SaveChangesAsync();

        return club.Id;
    }

    public async Task<IEnumerable<Club>> GetClubsAsync(string? search)
    {
        var query = _db.Clubs.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(c => EF.Functions.Like(c.Name, $"%{search}%"));

        return await query.ToListAsync();
    }

    public async Task<Event?> CreateEventAsync(Guid clubId, string title, string description, DateTime scheduledAt)
    {
        var club = await _db.Clubs.Include(c => c.Events).FirstOrDefaultAsync(c => c.Id == clubId);
        if (club == null)
            return null;

        var evt = club.ScheduleEvent(title, description, scheduledAt);
        _db.Events.Add(evt);
        await _db.SaveChangesAsync();

        return evt;
    }

    public async Task<IEnumerable<Event>> GetEventsAsync(Guid clubId)
    {
        var club = await _db.Clubs.Include(c => c.Events).FirstOrDefaultAsync(c => c.Id == clubId);
        return club?.Events ?? Enumerable.Empty<Event>();
    }
}
