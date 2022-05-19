namespace HilbertWeb.BackendApp.Dto.Permissions
{
    public class ManageUserRolesDto
    {
        public int UserId { get; set; } = -1;
        public string Email { get; set; } = "";
        public IList<UserRolesDto> UserRoles { get; set; } = new List<UserRolesDto>();
    }
}
