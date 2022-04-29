using Microsoft.AspNetCore.Identity;

namespace Spiderweb.App.Database.Seeds;

public static class DefaultRoles
{
    public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(Constants.DefaultRoles.Superman.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Constants.DefaultRoles.Pleb.ToString()));
    }
}