using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(My_Forum_Web.Startup))]
namespace My_Forum_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
