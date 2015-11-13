using System.Threading.Tasks;

namespace WindowsAuthentication.Middleware
{
    public interface IWindowsAuthenticationProvider
    {
        Task Authenticate(WindowsAuthenticatedContext context);
    }
}
