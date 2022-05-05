using HilbertWeb.BackendApp.Constants;
using HilbertWeb.BackendApp.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HilbertWeb.BackendApp.Database.Seeds;

public static class DefaultUsers
{
    public static async Task SeedBasicUserAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        var defaultUser = new ApplicationUser
        {
            UserName = "basicuser@gmail.com",
            Email = "basicuser@gmail.com",
            EmailConfirmed = true
        };
        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                await userManager.AddToRoleAsync(defaultUser, Constants.DefaultRoles.Superman.ToString());
            }
        }
    }
    public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        var defaultUser = new ApplicationUser
        {
            UserName = "superadmin@gmail.com",
            Email = "superadmin@gmail.com",
            EmailConfirmed = true
        };
        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                await userManager.AddToRoleAsync(defaultUser, Constants.DefaultRoles.Superman.ToString());
                await userManager.AddToRoleAsync(defaultUser, Constants.DefaultRoles.Pleb.ToString());
            }
            await roleManager.SeedClaimsForSuperAdmin();
        }
    }
    private async static Task SeedClaimsForSuperAdmin(this RoleManager<ApplicationRole> roleManager)
    {
        var adminRole = await roleManager.FindByNameAsync(Constants.DefaultRoles.Superman.ToString());
        await roleManager.AddPermissionClaim(adminRole, "News");
    }
    public static async Task AddPermissionClaim(this RoleManager<ApplicationRole> roleManager, ApplicationRole role, string module)
    {
        var allClaims = await roleManager.GetClaimsAsync(role);
        var allPermissions = Permissions.GeneratePermissionsForModule(module);
        foreach (var permission in allPermissions)
        {
            if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
    }
}
