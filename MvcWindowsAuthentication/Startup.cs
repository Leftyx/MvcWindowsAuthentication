using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcWindowsAuthentication.Startup))]
namespace MvcWindowsAuthentication
{
    using WindowsAuthentication.Middleware;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(WindowsAuthenticationDefaults.AuthenticationType);

            app.UseWindowsAuthentication(new WindowsAuthenticationOptions()
            {
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                SignInAsAuthenticationType = WindowsAuthenticationDefaults.AuthenticationType,
                AuthenticationType = WindowsAuthenticationDefaults.AuthenticationType,
                Provider = new WindowsAuthenticationProvider
                {
                    OnAuthenticated = context =>
                    {
                        // context.Identity is of type ClaimsIdentity and can be extended;

                        context.Identity.AddClaim(new Claim("app:name", "MvcWindowsAuthentication"));

                        return Task.FromResult(true);
                    }
                }
            });
        }
    }
}
