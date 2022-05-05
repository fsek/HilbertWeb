using Microsoft.AspNetCore.Authorization;

namespace HilbertWeb.BackendApp.Permission
{
    internal class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; private set; }
        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}
