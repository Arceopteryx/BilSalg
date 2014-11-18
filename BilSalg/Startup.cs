using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BilSalg.Startup))]
namespace BilSalg
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
