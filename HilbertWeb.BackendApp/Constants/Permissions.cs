namespace HilbertWeb.BackendApp.Constants
{
    public static class Permissions
    {
        public static List<string> PermissionsModules = new List<string>()
        {
            "News",
            "Users",
            "Permissions",
            "Committees"
        };

        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }

        public static List<string> AllPermissions()
        {
            return PermissionsModules
                .Select(x => GeneratePermissionsForModule(x))
                .Aggregate(new List<string>(), (acc,x) =>
                {
                    acc.AddRange(x);
                    return acc;
                });
        }
    }
}
