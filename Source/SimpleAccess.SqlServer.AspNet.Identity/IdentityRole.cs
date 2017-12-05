using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace SimpleAccess.SqlServer.AspNet.Identity
{
    [Entity("SA_AspNet_IdentityRoles")]
    public class IdentityRole : IRole
    {
        public IdentityRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        public IdentityRole(string name)
            : this()
        {
            Name = name;
        }

        public IdentityRole(string name, string id)
        {
            Name = name;
            Id = id;
        }

        public string Id { get; set; }

        public string Name { get; set; }
    }

    [Entity("SA_AspNet_IdentityRole")]
    public class IdentityRole<TKey, TUserRole> : IRole<TKey> 
        where TUserRole : IdentityUserRole<TKey>
    {
        public IdentityRole()
        {
        }

        public IdentityRole(string name)
            : this()
        {
            Name = name;
        }

        public IdentityRole(string name, TKey id)
        {
            Name = name;
            Id = id;
        }

        public TKey Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Navigation property for users in the role
        /// </summary>
        public virtual ICollection<TUserRole> Users { get; }


    }
}
