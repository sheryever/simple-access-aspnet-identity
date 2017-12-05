using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleAccess.MySql.AspNet.Identity.Example.Startup))]
namespace SimpleAccess.MySql.AspNet.Identity.Example
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
