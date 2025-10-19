namespace GameClubService.Domain.Entities;

public class Club
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string Description { get; private set; }
    private readonly List<Event> _events = [];
    public IReadOnlyCollection<Event> Events => _events.AsReadOnly();

    public Club(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Club name is required.");

        Name = name;
        Description = description;
    }


    public Event ScheduleEvent(string title, string description, DateTime scheduledAt)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Event title is required.");

        var evt = new Event(title, description, scheduledAt, Id);
        _events.Add(evt);
        return evt;
    }
}