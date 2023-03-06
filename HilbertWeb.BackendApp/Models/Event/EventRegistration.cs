namespace HilbertWeb.BackendApp.Models.Event;

public class EventRegistration
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public DateTime OpensAt { get; set; }
    public DateTime ClosesAt { get; set; }
    public int? MaxAttendees { get; set; }

    public ICollection<EventAttendee> attendees { get; set; }
}
