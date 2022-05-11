using HilbertWeb.BackendApp.Database;
using HilbertWeb.BackendApp.Database.Seeds;
using HilbertWeb.BackendApp.Models;
using HilbertWeb.BackendApp.Models.Identity;
using HilbertWeb.BackendApp.ViewModels;
using Mapster;
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
        [Authorize(Policy = "Permissions.Users.Edit")]
        public async Task<IActionResult> Index()
        {
            var allUsers = await _userManager.Users.ToListAsync();
            return Ok(allUsers.Adapt<UserViewModel[]>());
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Policy = "Permissions.Users.View")]
        public async Task<IActionResult> Index(int id)
        {
            var user = await _userManager.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user.Adapt<UserViewModel>());
        }
    }
}
