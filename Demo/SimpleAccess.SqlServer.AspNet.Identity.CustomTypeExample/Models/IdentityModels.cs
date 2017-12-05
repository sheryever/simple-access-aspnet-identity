using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SimpleAccess.SqlServer.AspNet.Identity;

namespace SimpleAccess.SqlServer.AspNet.Identity.CustomTypeExample.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<long, ApplicationIdentityUserLogin
        , ApplicationIdentityUserRole, ApplicationIdentityUserClaims>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, long> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string FullName { get; set; } = "Some Name";
    }

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext()
    //        : base("DefaultConnection", throwIfV1Schema: false)
    //    {
    //    }

    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }
    //}

    public class ApplicationIdentityUserRole : IdentityUserRole<long>
    {

    }

    public class ApplicationIdentityUserClaims : IdentityUserClaim<long>
    {

    }

    public class ApplicationIdentityUserLogin : IdentityUserLogin<long>
    {

    }

    public class ApplicationRole : IdentityRole<long, ApplicationIdentityUserRole>
    {

    }
}