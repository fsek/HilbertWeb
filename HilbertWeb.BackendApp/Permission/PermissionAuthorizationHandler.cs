using Microsoft.AspNetCore.Authorization;

namespace HilbertWeb.BackendApp.Permission
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        public PermissionAuthorizationHandler() { }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }
            var permissions = context.User.Claims.Where(x => x.Type == "Permission" &&
                                                                x.Value == $"{requirement.Permission}" &&
                                                                x.Issuer == "LOCAL AUTHORITY");

            if (permissions.Any())
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
