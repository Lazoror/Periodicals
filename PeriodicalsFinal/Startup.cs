using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PeriodicalsFinal.Startup))]
namespace PeriodicalsFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
