using System;
using Microsoft.AspNet.Identity;

namespace SimpleAccess.SqlServer.AspNet.Identity
{
    [Entity("SA_AspNet_IdentityUserRoles")]
    public class IdentityUserRole
    {
        /// <summary>
        /// UserId for the user that is in the role
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// RoleId for the role
        /// </summary>
        public string RoleId { get; set; }
    }

    [Entity("SA_AspNet_IdentityUserRoles")]
    public class IdentityUserRole<TKey>
    {
        /// <summary>
        /// UserId for the user that is in the role
        /// </summary>
        public TKey UserId { get; set; }
        /// <summary>
        /// RoleId for the role
        /// </summary>
        public TKey RoleId { get; set; }
    }
}
