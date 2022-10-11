using HilbertWeb.BackendApp.Models.Identity;
using HilbertWeb.BackendApp.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace HilbertWeb.BackendApp.Controllers.Account;

[ApiController]
public class AuthenticationController : ControllerBase
{
    private ILogger _logger;
    private UserManager<ApplicationUser> _userManager;
    private SignInManager<ApplicationUser> _signInManager;

    public AuthenticationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AuthenticationController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("api/authentication/login")]
    public async Task<ActionResult> Signin([FromBody] LoginDto model, [FromQuery] string? returnUrl = null)
    {
        returnUrl ??= Url.Content("/");
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            return Ok(returnUrl);
        }
        if (result.IsLockedOut)
        {
            _logger.LogWarning("User account locked out.");
            return StatusCode(418);
        }
        else
        {
            _logger.LogWarning("Unknown login failure.");
            return StatusCode(403);
        }
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("api/authentication/register")]
    public async Task<ActionResult> Register([FromBody] LoginDto model, [FromQuery] string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            // new account log
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
            //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            return Ok(new { email = model.Email, returnUrl });
        }

        return BadRequest(result.Errors);
    }

    [HttpPost]
    [Route("api/authentication/logout")]
    public async Task<IActionResult> Logout(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("/");

        await _signInManager.SignOutAsync();
        _logger.LogDebug("User logged out.");

        return Ok(returnUrl);
    }
}
