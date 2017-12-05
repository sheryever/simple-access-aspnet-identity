using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace SimpleAccess.SqlServer.AspNet.Identity
{

    [Entity("SA_AspNet_IdentityUserClaims")]
    public class IdentityUserClaim
    {
        [Identity]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }

    [Entity("SA_AspNet_IdentityUserClaims")]
    public class IdentityUserClaim<TKey>
    {
        [Identity]
        public int Id { get; set; }
        public TKey UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
