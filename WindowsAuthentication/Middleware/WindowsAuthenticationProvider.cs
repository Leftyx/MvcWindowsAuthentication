using System;
using System.Threading.Tasks;

namespace WindowsAuthentication.Middleware
{
    public class WindowsAuthenticationProvider : IWindowsAuthenticationProvider
    {
        public WindowsAuthenticationProvider()
        {
            OnAuthenticated = context => Task.FromResult<object>(null);
        }

        public Func<WindowsAuthenticatedContext, Task> OnAuthenticated { get; set; }

        public virtual Task Authenticate(WindowsAuthenticatedContext context)
        {
            return OnAuthenticated(context);
        }
    }
}