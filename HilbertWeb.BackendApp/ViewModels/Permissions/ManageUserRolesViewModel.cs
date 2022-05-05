namespace HilbertWeb.BackendApp.ViewModels.Permissions
{
    public class ManageUserRolesViewModel
    {
        public int UserId { get; set; } = -1;
        public string Email { get; set; } = "";
        public IList<UserRolesViewModel> UserRoles { get; set; } = new List<UserRolesViewModel>();
    }
}
