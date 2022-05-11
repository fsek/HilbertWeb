using HilbertWeb.BackendApp.ViewModels.Permissions;

namespace HilbertWeb.BackendApp.ViewModels;

public class AdvancedUserViewModel
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> Permissions { get; set; }
}
