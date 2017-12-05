using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleAccess.SqlServer.AspNet.Identity.Example.Startup))]
namespace SimpleAccess.SqlServer.AspNet.Identity.Example
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
