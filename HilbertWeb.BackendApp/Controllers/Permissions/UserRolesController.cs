using HilbertWeb.BackendApp.Models.Identity;
using HilbertWeb.BackendApp.ViewModels.Permissions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HilbertWeb.BackendApp.Controllers.Permissions
{
    [ApiController]
    [Route("permissions/userroles")]
    public class UserRolesController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public UserRolesController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("{email}")]
        public async Task<IActionResult> Index(string email)
        {
            var viewModel = new List<UserRolesViewModel>();
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("User not found.");

            foreach (var role in _roleManager.Roles.ToList())
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                viewModel.Add(userRolesViewModel);
            }

            // TODO: make sure to set smaller validation interval like 10 minutes:
            // https://stackoverflow.com/questions/19487322/what-is-asp-net-identitys-iusersecuritystampstoretuser-interface/19505060#19505060
            // can also implement our own onValidate to make sure it happends everytime

            // this logs out the user next time its cookie needs to be validated
            await _userManager.UpdateSecurityStampAsync(user); 

            var model = new ManageUserRolesViewModel()
            {
                UserId = user.Id,
                Email = user.Email,
                UserRoles = viewModel
            };
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, ManageUserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            result = await _userManager.AddToRolesAsync(user, model.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));
            var currentUser = await _userManager.GetUserAsync(User);
            await _signInManager.RefreshSignInAsync(currentUser);
            return Ok(new { userId = id });
        }
    }
}
