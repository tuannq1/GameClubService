using GameClubService.Domain.Common;
using GameClubService.Domain.Entities;

namespace GameClubService.Domain.Interfaces;

public interface IClubRepository
{
    Task<(Guid?, PersistenceStatusEnum)> CreateClubAsync(string name, string? description);
    Task<(IEnumerable<Club>?, PersistenceStatusEnum)> GetClubsAsync(string? search);
    Task<(Event?, PersistenceStatusEnum)> CreateEventAsync(Guid clubId, string title, string? description, DateTime scheduledAt);
    Task<(IEnumerable<Event>?, PersistenceStatusEnum)> GetEventsAsync(Guid clubId);
}