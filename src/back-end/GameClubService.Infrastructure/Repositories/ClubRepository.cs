using GameClubService.Domain.Common;
using GameClubService.Domain.Entities;
using GameClubService.Domain.Interfaces;
using GameClubService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameClubService.Infrastructure.Repositories;

public class ClubRepository(GameClubDbContext db) : IClubRepository
{
    private readonly GameClubDbContext _db = db;

    public async Task<(Guid?, PersistenceStatusEnum)> CreateClubAsync(string name, string? description)
    {
        if (await _db.Clubs.AnyAsync(c => c.Name == name))
            return (null, PersistenceStatusEnum.Conflict);

        var club = new Club(name, description);
        _db.Clubs.Add(club);
        await _db.SaveChangesAsync();

        return (club.Id, PersistenceStatusEnum.Success);
    }

    public async Task<(IEnumerable<Club>?, PersistenceStatusEnum)> GetClubsAsync(string? search)
    {
         var query = _db.Clubs.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(c => EF.Functions.Like(c.Name, $"%{search}%"));

        var clubs = await query.ToListAsync();

        return (clubs, PersistenceStatusEnum.Success);
    }

    public async Task<(Event?, PersistenceStatusEnum)> CreateEventAsync(Guid clubId, string title, string? description, DateTime scheduledAt)
    {
        var club = await _db.Clubs.Include(c => c.Events).FirstOrDefaultAsync(c => c.Id == clubId);
        if (club == null)
            return (null, PersistenceStatusEnum.NotFound);

        var evt = club.ScheduleEvent(title, description, scheduledAt);
        _db.Events.Add(evt);
        await _db.SaveChangesAsync();

        return (evt, PersistenceStatusEnum.Success);
    }

    public async Task<(IEnumerable<Event>?, PersistenceStatusEnum)> GetEventsAsync(Guid clubId)
    {
        var club = await _db.Clubs.Include(c => c.Events).FirstOrDefaultAsync(c => c.Id == clubId);
        if (club == null)
            return (null, PersistenceStatusEnum.NotFound);

        var events = club?.Events ?? Enumerable.Empty<Event>();
        return (events, PersistenceStatusEnum.Success);
    }
}
