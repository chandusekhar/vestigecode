using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WSS.CustomerApplication.Startup))]

namespace WSS.CustomerApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
