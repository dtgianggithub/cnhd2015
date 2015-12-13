using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ESClient.Startup))]
namespace ESClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
