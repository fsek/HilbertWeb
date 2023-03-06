namespace HilbertWeb.BackendApp.Models.Event;

public class EventAttendee
{
    public Guid Id { get; set; }
    public Guid EventRegistrationId { get; set; }
    public Guid UserId { get; set; }
    public bool? DrinkPackage { get; set; }
}
