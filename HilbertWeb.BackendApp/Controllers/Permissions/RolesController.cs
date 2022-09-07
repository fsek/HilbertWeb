using HilbertWeb.BackendApp.Database;
using HilbertWeb.BackendApp.Dto.Permissions;
using HilbertWeb.BackendApp.Models;
using HilbertWeb.BackendApp.Models.Identity;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HilbertWeb.BackendApp.Controllers.Permissions
{
    [ApiController]
    [Route("permissions/roles")]
    public class RolesController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        public RolesController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return Ok(roles.Adapt<List<RolesDto>>());
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new ApplicationRole(roleName.Trim()));
                return Ok();
            }

            return BadRequest();
        }
    }
}
