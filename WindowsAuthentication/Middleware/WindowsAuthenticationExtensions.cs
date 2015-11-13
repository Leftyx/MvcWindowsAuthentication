using System;
using Owin;

namespace WindowsAuthentication.Middleware
{
    public static class WindowsAuthenticationExtensions
    {
        public static IAppBuilder UseWindowsAuthentication(this IAppBuilder app, WindowsAuthenticationOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }
            
            return app.Use(typeof(WindowsAuthenticationMiddleware), app, options);
        }
    }
}