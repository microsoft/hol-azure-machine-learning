using Owin;
using System.Configuration;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

namespace powerbi_sample_app
{
    public partial class Startup
    {
        public void ConfigurePowerBI(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions { });
        }
    }
}