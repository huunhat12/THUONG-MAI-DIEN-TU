using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QLBANDTDD.Startup))]
namespace QLBANDTDD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
