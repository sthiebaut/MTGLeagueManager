using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MTGLeagueManager.Front.Startup))]
namespace MTGLeagueManager.Front
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
