using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CompanyPortal.Startup))]
namespace CompanyPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
