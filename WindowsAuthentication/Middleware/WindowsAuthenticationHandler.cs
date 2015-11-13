using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;

namespace WindowsAuthentication.Middleware
{
    public class WindowsAuthenticationHandler : AuthenticationHandler<WindowsAuthenticationOptions>
    {
        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {

            if (string.IsNullOrEmpty(Context.Request.User.Identity.Name))
            {
                Context.Response.StatusCode = 401;
                return null;
            }

            AuthenticationProperties properties = UnpackStateParameter(Request.Query);

            // var authorizationHeader = Request.Headers["Authorization"];

            try
            {
                // var windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
                ClaimsIdentity windowsIdentity = (ClaimsIdentity)Context.Request.User.Identity;

                var context = new WindowsAuthenticatedContext(Context, windowsIdentity, properties);
                
                await Options.Provider.Authenticate(context);
                
                return new AuthenticationTicket(windowsIdentity, properties);

            }
            catch (Exception)
            {
                return new AuthenticationTicket(null, (AuthenticationProperties)null);
            }

        }

        public override async Task<bool> InvokeAsync()
        {
            if (string.IsNullOrEmpty(Context.Request.User.Identity.Name))
            {
                await Response.WriteAsync(@"<div>User Not Authorized!</div>");
                Response.StatusCode = 401;
                return true;
            }

            var ticket = await AuthenticateAsync();

            if (ticket == null || ticket.Identity == null)
            {
                Response.StatusCode = 401;
                return true;
            }

            Context.Authentication.SignIn(ticket.Properties, ticket.Identity);

            return false;

        }

        private AuthenticationProperties UnpackStateParameter(IReadableStringCollection query)
        {
            string state = GetParameter(query, "state");
            if (state != null)
            {
                return Options.StateDataFormat.Unprotect(state);
            }
            return null;
        }

        private static string GetParameter(IReadableStringCollection query, string key)
        {
            IList<string> values = query.GetValues(key);
            if (values != null && values.Count == 1)
            {
                return values[0];
            }
            return null;
        }
    }
}