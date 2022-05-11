using HilbertWeb.BackendApp.Models.Identity;
using HilbertWeb.BackendApp.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HilbertWeb.BackendApp.Controllers.Account;

[ApiController]
public class AccountController : ControllerBase
{
    private UserManager<ApplicationUser> _userManager;
    private RoleManager<ApplicationRole> _roleManager;
    private ILogger _logger;

    public AccountController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ILogger<AuthenticationController> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    /// <summary>
    /// Gets info about current user
    /// </summary>
    [HttpGet]
    [Authorize]
    [Route("api/account/user")]
    public async Task<IActionResult> GetUser()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null)
            return Ok();

        var dto = currentUser.Adapt<AdvancedUserViewModel>();

        // gets claim values from roleclaims from role from userroles :)
        var claims = currentUser.UserRoles.Select(x => x.Role).SelectMany(x => x.RoleClaims).Select(x => x.ClaimValue);

        dto.Permissions = claims.ToHashSet().ToList(); // this is stupid... but I think its fast

        return Ok(dto);
    }
}
