using System.Security.Claims;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;

namespace WindowsAuthentication.Middleware
{

    public class WindowsAuthenticatedContext : BaseContext
    {
        public WindowsAuthenticatedContext(IOwinContext context, ClaimsIdentity identity, AuthenticationProperties properties): base(context)
        {
            this.Identity = identity;
            this.Properties = properties;
        }

        public ClaimsIdentity Identity { get; private set; }
        public AuthenticationProperties Properties { get; private set; }
    }
}