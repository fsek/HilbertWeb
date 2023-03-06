using HilbertWeb.BackendApp.Models.Identity;
namespace HilbertWeb.BackendApp.Models.Event;

public class Event
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public string? Description { get; set; }
    public string? Place { get; set; }
    public ApplicationUser ContactUser { get; set; }
}
