using HilbertWeb.BackendApp.Dto.Permissions;

namespace HilbertWeb.BackendApp.Dto;

public class CommitteeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int GuildId { get; set; }
}
