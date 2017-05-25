using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Identity.Infrastructure
{
    public class RoleAppManager : RoleManager<RoleApp>
    {
        public RoleAppManager(IRoleStore<RoleApp, string> store) : base(store)
        {
        }
        public static RoleAppManager Create(IdentityFactoryOptions<RoleAppManager>identityFactoryOptions, IOwinContext owinContext)
        {
            return new RoleAppManager(new RoleStore<RoleApp>(owinContext.Get<UserAppDbContext>()));
        }
    }
}