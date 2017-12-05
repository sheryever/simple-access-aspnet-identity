using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace SimpleAccess.SqlServer.AspNet.Identity
{
   

    [Entity("SA_AspNet_IdentityUserLogins")]
    public class IdentityUserLogin
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }

    [Entity("SA_AspNet_IdentityUserLogins")]
    public class IdentityUserLogin<TKey>
    {
        public TKey Id { get; set; }
        public TKey UserId { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }
}
