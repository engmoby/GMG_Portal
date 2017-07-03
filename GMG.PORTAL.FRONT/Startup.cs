using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GMG.PORTAL.FRONT.Startup))]
namespace GMG.PORTAL.FRONT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
