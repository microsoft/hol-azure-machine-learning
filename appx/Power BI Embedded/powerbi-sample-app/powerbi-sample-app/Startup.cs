using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(powerbi_sample_app.Startup))]
namespace powerbi_sample_app
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigurePowerBI(app);
        }
    }
}
