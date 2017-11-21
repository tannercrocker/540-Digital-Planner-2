using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Digital_Planner.Startup))]
namespace Digital_Planner
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
