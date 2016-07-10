using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GBAdmin.Web.Startup))]
namespace GBAdmin.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
