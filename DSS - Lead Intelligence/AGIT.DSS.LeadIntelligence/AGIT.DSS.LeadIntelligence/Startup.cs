using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AGIT.DSS.LeadIntelligence.Startup))]
namespace AGIT.DSS.LeadIntelligence
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
