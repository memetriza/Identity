using Identity.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Identity.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public UserManagerApp UserManagerApp
        {
            get
            {
                IOwinContext context = HttpContext.GetOwinContext();
                return context.GetUserManager<UserManagerApp>();
            }
        }
        public ActionResult UyeOl()
        {
            return View(new UserApp());
        }
        [HttpPost]
        public async Task<ActionResult> UyeOl(UserApp user, string password)
        {
            UserApp u = new UserApp();
            u.Email = user.Email;
            u.UserName = user.UserName;
            u.Name = user.Name;
            u.Surname = user.Surname;
            u.Sha512Pass = Helpers.GetHashPass(password);
            u.UyeMi = true;

            IdentityResult result = await UserManagerApp.CreateAsync(u, password);
            if (result.Succeeded)
            {
                FormsAuthentication.SetAuthCookie(user.Email, true);
                return RedirectToRoute("Home");
            }
            else
            {
                foreach (var item in result.Errors.ToList())
                {
                    ModelState.AddModelError("", item);
                }
                return View();
            }
        }
    }
}