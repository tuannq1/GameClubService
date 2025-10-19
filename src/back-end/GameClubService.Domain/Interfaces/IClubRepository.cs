using GameClubService.Domain.Entities;

namespace GameClubService.Domain.Interfaces;

public interface IClubRepository
{
    Task<Guid?> CreateClubAsync(string name, string description);
    Task<IEnumerable<Club>> GetClubsAsync(string? search);
    Task<Event?> CreateEventAsync(Guid clubId, string title, string description, DateTime scheduledAt);
    Task<IEnumerable<Event>> GetEventsAsync(Guid clubId);
}