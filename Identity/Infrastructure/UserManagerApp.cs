using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;


namespace Identity.Infrastructure
{
    public class UserManagerApp : UserManager<UserApp>
    {
        public UserManagerApp(IUserStore<UserApp> store) : base(store)
        {
        }

        public static UserManagerApp Create(IdentityFactoryOptions<UserManagerApp> identityFactoryOptions, Microsoft.Owin.IOwinContext owinContext)
        {
            UserAppDbContext context = owinContext.Get<UserAppDbContext>();
            UserManagerApp user = new UserManagerApp(new UserStore<UserApp>(context));

            PasswordValidator pass = new PasswordValidator
            {
                RequiredLength = 6,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonLetterOrDigit = false,
                RequireUppercase = true,
            };

            UserValidator<UserApp> userValidator = new UserValidator<UserApp>(user)
            {
                RequireUniqueEmail = true,
                AllowOnlyAlphanumericUserNames = true
            };

            user.PasswordValidator = pass;
            user.UserValidator = userValidator;

            return user;
        }
    }
}