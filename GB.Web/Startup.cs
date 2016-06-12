using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GB.Web.Startup))]
namespace GB.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
