using Microsoft.AspNetCore.Identity;

namespace HilbertWeb.BackendApp.Models.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }

    /// <summary>
    /// Alright i did some special stuffs with the following:
    /// UserRoles always gets loaded (because I define AutoInclude in ApplicationDbContext for it)
    /// but the other ones I don't. Then you may ask: why are the other stuff here? Well, in case
    /// they are needed in the future they might as well be here.
    /// But you will find that Claims, Logins and Tokens will be null.
    /// </summary>

    public virtual ICollection<ApplicationUserClaim> Claims { get; set; }
    public virtual ICollection<ApplicationUserLogin> Logins { get; set; }
    public virtual ICollection<ApplicationUserToken> Tokens { get; set; }
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
}

public class ApplicationRole : IdentityRole<int>
{
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
    public ApplicationRole() : base() { }
    public ApplicationRole(string roleName) : base(roleName) { }
}

public class ApplicationUserRole : IdentityUserRole<int>
{
    public virtual ApplicationUser User { get; set; }
    public virtual ApplicationRole Role { get; set; }
}

public class ApplicationUserClaim : IdentityUserClaim<int>
{
    public virtual ApplicationUser User { get; set; }
}

public class ApplicationUserLogin : IdentityUserLogin<int>
{
    public virtual ApplicationUser User { get; set; }
}

public class ApplicationRoleClaim : IdentityRoleClaim<int>
{
    public virtual ApplicationRole Role { get; set; }
}

public class ApplicationUserToken : IdentityUserToken<int>
{
    public virtual ApplicationUser User { get; set; }
}
