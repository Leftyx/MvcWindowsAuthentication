using Microsoft.Owin.Security;

namespace WindowsAuthentication.Middleware
{
    public class WindowsAuthenticationOptions : AuthenticationOptions
    {
        public WindowsAuthenticationOptions(): base(WindowsAuthenticationDefaults.AuthenticationType)
        {
            Description.Caption = WindowsAuthenticationDefaults.AuthenticationType;
            AuthenticationMode = AuthenticationMode.Active;
        }

        public string SignInAsAuthenticationType { get; set; }

        public ISecureDataFormat<AuthenticationProperties> StateDataFormat { get; set; }

        public IWindowsAuthenticationProvider Provider { get; set; }

    }
}
