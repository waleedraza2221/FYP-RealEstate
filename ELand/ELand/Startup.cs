using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ELand.Startup))]
namespace ELand
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
