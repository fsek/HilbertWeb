using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HilbertWeb.BackendApp.Models.Identity
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole(): base() { }
        public ApplicationRole(string roleName): base(roleName) { }
    }
}
