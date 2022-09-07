using System.ComponentModel.DataAnnotations;

namespace HilbertWeb.BackendApp.Dto;

public class ManageCommitteeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int GuildId { get; set; }
}
