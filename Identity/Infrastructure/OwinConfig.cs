using Microsoft.Owin;
using Owin;
using Identity.Infrastructure;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

//[assembly: OwinStartup(typeof(Identity.Infrastructure.OwinConfig))]

namespace Identity
{
    public class OwinConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new UserAppDbContext());
            app.CreatePerOwinContext<UserManagerApp>(UserManagerApp.Create);
            app.CreatePerOwinContext<RoleAppManager>(RoleAppManager.Create);

            CookieAuthenticationProvider provider = new CookieAuthenticationProvider();

            //PathString=>route provider
            var originalHandler = provider.OnApplyRedirect;
            provider.OnApplyRedirect = context =>
            {
                string NewURI = "login";
                context.RedirectUri = NewURI;
                originalHandler.Invoke(context);
            };

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType=DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath= new PathString("/Account/Login"),
                Provider = provider
            });
        }

    }
}
