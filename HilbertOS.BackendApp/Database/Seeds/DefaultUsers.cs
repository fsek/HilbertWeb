using Microsoft.AspNetCore.Identity;
using Spiderweb.App.Constants;
using System.Security.Claims;

namespace Spiderweb.App.Database.Seeds;

public static class DefaultUsers
{
    public static async Task SeedBasicUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var defaultUser = new IdentityUser
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
    public static async Task SeedSuperAdminAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var defaultUser = new IdentityUser
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
    private async static Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager)
    {
        var adminRole = await roleManager.FindByNameAsync("SuperAdmin");
        await roleManager.AddPermissionClaim(adminRole, "Products");
    }
    public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
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
