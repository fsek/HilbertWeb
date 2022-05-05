using HilbertWeb.BackendApp.Database;
using HilbertWeb.BackendApp.Database.Seeds;
using HilbertWeb.BackendApp.Models;
using HilbertWeb.BackendApp.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HilbertWeb.BackendApp.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allUsers = await _userManager.Users.ToListAsync();
            return Ok(allUsers);
        }
    }
}