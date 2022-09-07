using HilbertWeb.BackendApp.Models.Identity;

namespace HilbertWeb.BackendApp.Models;

public class Committee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int GuildId { get; set; }
}
