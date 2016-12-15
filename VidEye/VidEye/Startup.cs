using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VidEye.Startup))]
namespace VidEye
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
