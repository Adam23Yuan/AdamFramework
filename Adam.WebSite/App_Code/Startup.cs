using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Adam.WebSite.Startup))]
namespace Adam.WebSite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
