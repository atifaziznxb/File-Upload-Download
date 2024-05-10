using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestFileUpload.Startup))]
namespace TestFileUpload
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
