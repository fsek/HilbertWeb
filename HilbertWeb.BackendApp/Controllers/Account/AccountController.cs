using HilbertWeb.BackendApp.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HilbertWeb.BackendApp.Controllers.Account;

[ApiController]
public class AccountController : ControllerBase
{
    private ILogger _logger;
    private UserManager<ApplicationUser> _userManager;

    public AccountController(UserManager<ApplicationUser> userManager, ILogger<AuthenticationController> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    // TODO: fix this shit
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

        return Ok(new {
            LoggedIn = true,
            Email = currentUser.Email
        });
    }
}
