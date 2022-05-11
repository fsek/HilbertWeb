using HilbertWeb.BackendApp.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace HilbertWeb.BackendApp.Database.Seeds;

public static class DefaultRoles
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        await roleManager.CreateAsync(new ApplicationRole(Constants.DefaultRoles.Superman.ToString()));
        await roleManager.CreateAsync(new ApplicationRole(Constants.DefaultRoles.UnionMember.ToString()));
    }
}
