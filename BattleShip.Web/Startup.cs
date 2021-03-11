using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BattleShip.Web.Startup))]
namespace BattleShip.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
