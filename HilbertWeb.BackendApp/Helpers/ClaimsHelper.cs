using HilbertWeb.BackendApp.Models.Identity;
using HilbertWeb.BackendApp.Dto.Permissions;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using System.Security.Claims;

namespace HilbertWeb.BackendApp.Helpers
{
    public static class ClaimsHelper
    {
        public static void GetPermissions(this List<RoleClaimsDto> allPermissions, List<string> policies)
        {
            foreach (string policy in policies)
            {
                allPermissions.Add(new RoleClaimsDto { Value = policy, Type = "Permissions" });
            }
        }
        public static async Task AddPermissionClaim(this RoleManager<ApplicationRole> roleManager, ApplicationRole role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
    }
}
