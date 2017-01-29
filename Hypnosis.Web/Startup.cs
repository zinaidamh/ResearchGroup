using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hypnosis.Startup))]
namespace Hypnosis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
