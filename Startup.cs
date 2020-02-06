using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(galleryprojectb.Startup))]
namespace galleryprojectb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
