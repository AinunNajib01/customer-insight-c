using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AI.ADP.UpdateOpportunity.Startup))]
namespace AI.ADP.UpdateOpportunity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
