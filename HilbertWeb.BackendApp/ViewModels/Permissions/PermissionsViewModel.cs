namespace HilbertWeb.BackendApp.ViewModels.Permissions
{
    public class PermissionViewModel
    {
        public int RoleId { get; set; } = -1;
        public string RoleName { get; set; } = "";
        public IList<RoleClaimsViewModel> RoleClaims { get; set; } = new List<RoleClaimsViewModel>();
    }
}
