using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace SimpleAccess.SqlServer.AspNet.Identity
{
    [Entity("SA_AspNet_IdentityUsers")]
    public class IdentityUser : IUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public int AccessFailedCount { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTime? lockoutEndDateUtc { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public virtual ICollection<string> Roles { get; set; }
        public virtual ICollection<IdentityUserClaim> Claims { get; set; }
        public virtual List<UserLoginInfo> Logins { get; set; }

        public IdentityUser()
        {
            this.Claims = new List<IdentityUserClaim>();
            this.Roles = new List<string>();
            this.Logins = new List<UserLoginInfo>();
            this.Id = Guid.NewGuid().ToString();
            LockoutEnabled = true;
        }

        public IdentityUser(string userName)
            : this()
        {
            this.UserName = userName;
        }
    }

    [Entity("SA_AspNet_IdentityUsers")]
    public class IdentityUser<TKey, TLogin, TRole, TClaim> : IUser<TKey>
        where TLogin : IdentityUserLogin<TKey>
        where TRole : IdentityUserRole<TKey>
        where TClaim : IdentityUserClaim<TKey>
    {
        public TKey Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public int AccessFailedCount { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTime? lockoutEndDateUtc { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public virtual ICollection<string> Roles { get; set; }
        public virtual ICollection<IdentityUserClaim<TKey>> Claims { get; set; }
        public virtual List<UserLoginInfo> Logins { get; set; }

        public IdentityUser()
        {
            this.Claims = new List<IdentityUserClaim<TKey>>();
            this.Roles = new List<string>();
            this.Logins = new List<UserLoginInfo>();
            LockoutEnabled = true;
        }

        public IdentityUser(string userName)
            : this()
        {
            this.UserName = userName;
        }
    }



}
