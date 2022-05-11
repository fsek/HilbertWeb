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
            FirstName = "Spindel",
            LastName = "Man",
            UserName = "spindelman@fsektionen.se",
            Email = "spindelman@fsektionen.se",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                var result = await userManager.CreateAsync(defaultUser, "Password123");
                await userManager.AddToRoleAsync(defaultUser, Constants.DefaultRoles.UnionMember.ToString());
            }
            await roleManager.SeedClaimsForBasicUser();
        }
    }
    public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        var defaultUser = new ApplicationUser
        {
            FirstName = "Hilbert",
            LastName = "Admin-Älg",
            UserName = "admin@fsektionen.se",
            Email = "admin@fsektionen.se",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "Password123");
                await userManager.AddToRoleAsync(defaultUser, Constants.DefaultRoles.Superman.ToString());
                await userManager.AddToRoleAsync(defaultUser, Constants.DefaultRoles.UnionMember.ToString());
            }
            await roleManager.SeedClaimsForSuperAdmin();
        }
    }
    private async static Task SeedClaimsForBasicUser(this RoleManager<ApplicationRole> roleManager)
    {
        var adminRole = await roleManager.FindByNameAsync(Constants.DefaultRoles.Superman.ToString());
        await roleManager.AddPermissionClaims(adminRole, Permissions.GeneratePermissionsForModule("News"));
    }

    private async static Task SeedClaimsForSuperAdmin(this RoleManager<ApplicationRole> roleManager)
    {
        var adminRole = await roleManager.FindByNameAsync(Constants.DefaultRoles.Superman.ToString());
        await roleManager.AddPermissionClaims(adminRole, Permissions.AllPermissions());
    }

    public static async Task AddPermissionClaims(this RoleManager<ApplicationRole> roleManager, ApplicationRole role, List<string> permissions)
    {
        var allClaims = await roleManager.GetClaimsAsync(role);
        foreach (var permission in permissions)
        {
            if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
    }
}
