using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameClubService.Domain.Entities;

[Table("Events")]
public class Event
{
    [Key]
    public Guid Id { get; private set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    public string Title { get; private set; } = default!;

    [MaxLength(500)]
    public string? Description { get; private set; }

    [Required]
    public DateTime ScheduledAt { get; private set; }

    [ForeignKey(nameof(Club))]
    public Guid ClubId { get; private set; }

    public virtual Club Club { get; private set; } = default!;

    private Event() { }

    public Event(string title, string? description, DateTime scheduledAt, Guid clubId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Event title is required.", nameof(title));

        Title = title.Trim();
        Description = description?.Trim();
        ScheduledAt = scheduledAt;
        ClubId = clubId;
    }
}
