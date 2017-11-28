using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Digital_Planner_2.Startup))]
namespace Digital_Planner_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
