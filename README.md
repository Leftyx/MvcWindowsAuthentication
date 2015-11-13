# ASP.NET MVC Windows Authentication

OWIN middleware implementation for Windows Authentication.

### IIS / IIS Express

1. Disable Anonymous Authentication
2. Enable Windows Authenticaion

### Web.config

1. Remove the authentication section
2. Remove FormsAuthentication

  ```XML
  <system.web>
    <!--<authentication mode="Windows" />-->
    <compilation debug="true" targetFramework="4.5.2" />
    <httpRuntime targetFramework="4.5.2" />
  </system.web>
  <system.webServer>
    <modules>
      <remove name="FormsAuthentication" />
    </modules>
  </system.webServer>
  <runtime>
  ```
------

# Usage

Use this code in your **Startup**

  ```C#

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
  ```
------
