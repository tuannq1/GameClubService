namespace GameClubService.Domain.Entities;

public class Event
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime ScheduledAt { get; private set; }
    public Guid ClubId { get; private set; }

    public Event(string title, string description, DateTime scheduledAt, Guid clubId)
    {
        if (string.IsNullOrWhiteSpace(title))
        throw new ArgumentException("Event title is required.");

        Title = title;
        Description = description;
        ScheduledAt = scheduledAt;
        ClubId = clubId;
    }
}