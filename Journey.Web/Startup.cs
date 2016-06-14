using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Journey.Web.Startup))]
namespace Journey.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
