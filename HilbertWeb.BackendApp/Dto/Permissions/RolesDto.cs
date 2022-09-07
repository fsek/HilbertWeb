namespace HilbertWeb.BackendApp.Dto.Permissions;

public class RolesDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int GuildId { get; set; }
}
