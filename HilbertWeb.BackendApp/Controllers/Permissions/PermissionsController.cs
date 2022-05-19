using HilbertWeb.BackendApp.Helpers;
using HilbertWeb.BackendApp.Models.Identity;
using HilbertWeb.BackendApp.Dto.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HilbertWeb.BackendApp.Controllers.Permissions
{
    [ApiController]
    [Route("api/permissions/permission")]
    public class PermissionController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public PermissionController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }
        

        // TODO: not working lmao
        [HttpGet]
        [Authorize(Policy = "Permissions.Permissions.View")]
        public async Task<ActionResult> Index()
        {
            var result = new List<PermissionDto>();
            var allPermissions = new List<RoleClaimsDto>();
            allPermissions.GetPermissions(Constants.Permissions.AllPermissions());

            var allRoles = await _roleManager.Roles.ToListAsync();
            foreach(var role in allRoles)
            {
                var model = new PermissionDto();

                model.RoleId = role.Id;
                model.RoleName = role.Name;
                var claims = await _roleManager.GetClaimsAsync(role);
                var allClaimValues = allPermissions.Select(a => a.Value).ToList();
                var roleClaimValues = claims.Select(a => a.Value).ToList();
                var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
                foreach (var permission in allPermissions)
                {
                    if (authorizedClaims.Any(a => a == permission.Value))
                    {
                        permission.Selected = true;
                    }
                }
                model.RoleClaims = allPermissions;

                result.Add(model);
            }

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Policy = "Permissions.Permissions.View")]
        [Route("{roleId}")]
        public async Task<ActionResult> Index(int roleId)
        {
            var model = new PermissionDto();
            var allPermissions = new List<RoleClaimsDto>();
            allPermissions.GetPermissions(Constants.Permissions.AllPermissions());
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            model.RoleId = roleId;
            var claims = await _roleManager.GetClaimsAsync(role);
            var allClaimValues = allPermissions.Select(a => a.Value).ToList();
            var roleClaimValues = claims.Select(a => a.Value).ToList();
            var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
            foreach (var permission in allPermissions)
            {
                if (authorizedClaims.Any(a => a == permission.Value))
                {
                    permission.Selected = true;
                }
            }
            model.RoleClaims = allPermissions;
            return Ok(model);
        }

        [HttpPost]
        [Authorize(Policy = "Permissions.Permissions.Edit")]
        public async Task<IActionResult> Update(PermissionDto model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId.ToString());
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
            var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();
            foreach (var claim in selectedClaims)
            {
                await _roleManager.AddPermissionClaim(role, claim.Value);
            }
            return Ok(new { roleId = model.RoleId });
        }
    }
}