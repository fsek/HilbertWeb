namespace HilbertWeb.BackendApp.Dto.Permissions
{
    public class PermissionDto
    {
        public int RoleId { get; set; } = -1;
        public string RoleName { get; set; } = "";
        public IList<RoleClaimsDto> RoleClaims { get; set; } = new List<RoleClaimsDto>();
    }
}
