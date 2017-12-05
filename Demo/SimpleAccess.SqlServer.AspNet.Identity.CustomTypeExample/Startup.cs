using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleAccess.SqlServer.AspNet.Identity.CustomTypeExample.Startup))]
namespace SimpleAccess.SqlServer.AspNet.Identity.CustomTypeExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
