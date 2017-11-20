using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_540_Digital_Planner_2.Startup))]
namespace _540_Digital_Planner_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
