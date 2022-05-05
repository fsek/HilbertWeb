using HilbertWeb.BackendApp.Helpers;
using HilbertWeb.BackendApp.Models.Identity;
using HilbertWeb.BackendApp.ViewModels.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        [Authorize(Policy = "Permissions.View")]
        [Route("{roleId}")]
        public async Task<ActionResult> Index(int roleId)
        {
            var model = new PermissionViewModel();
            var allPermissions = new List<RoleClaimsViewModel>();
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
        [Authorize(Policy = "Permissions.Edit")]
        public async Task<IActionResult> Update(PermissionViewModel model)
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