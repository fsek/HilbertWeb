using HilbertWeb.BackendApp.Dto.Permissions;

namespace HilbertWeb.BackendApp.Dto;

public class AdvancedUserDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> Permissions { get; set; }
}
