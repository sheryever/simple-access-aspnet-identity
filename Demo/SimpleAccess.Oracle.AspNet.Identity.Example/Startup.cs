using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleAccess.Oracle.AspNet.Identity.Example.Startup))]
namespace SimpleAccess.Oracle.AspNet.Identity.Example
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
