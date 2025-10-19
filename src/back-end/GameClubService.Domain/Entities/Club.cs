using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameClubService.Domain.Entities;

[Table("Clubs")]
public class Club
{
    [Key]
    public Guid Id { get; private set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    public string Name { get; private set; } = default!;

    [MaxLength(500)]
    public string? Description { get; private set; }

    private readonly List<Event> _events = [];
    public IReadOnlyCollection<Event> Events => _events.AsReadOnly();

    private Club() { }

    public Club(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Club name is required.", nameof(name));

        Name = name.Trim();
        Description = description?.Trim();
    }

    public Event ScheduleEvent(string title, string? description, DateTime scheduledAt)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Event title is required.", nameof(title));

        var evt = new Event(title, description, scheduledAt, Id);
        _events.Add(evt);
        return evt;
    }
}
