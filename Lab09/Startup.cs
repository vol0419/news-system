using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lab09.Startup))]
namespace Lab09
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
