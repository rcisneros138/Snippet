using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Snippets.Startup))]
namespace Snippets
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
