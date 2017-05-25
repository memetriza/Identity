using Identity.Infrastructure;
using Identity.ViewModels;
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
    public class AccountController : Controller
    {
        public UserManagerApp UserManagerApp
        {
            get
            {
                IOwinContext context = HttpContext.GetOwinContext();
                return context.GetUserManager<UserManagerApp>();
            }
        }
        // GET: Account
        //[Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(UserManagerApp.Users);
        }
        //[Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View(new UserApp());
        }
       // [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Create(UserApp user,string password)
        {
            UserApp u = new UserApp();
            u.Email = user.Email;
            u.UserName = user.UserName;
            u.Name = user.Name;
            u.Surname = user.Surname;
            u.Sha512Pass = Helpers.GetHashPass(password);
            u.UyeMi = true;

            IdentityResult result= await UserManagerApp.CreateAsync(u, password);
            if (result.Succeeded)
            {
                return RedirectToRoute("uyeler");
            }
            else
            {
                foreach(var item in result.Errors.ToList())
                {
                    ModelState.AddModelError("", item);
                }
                return View();
            }
        }
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(string id)
        {
            
            UserApp user = await UserManagerApp.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                ModelState.AddModelError("", "Böyle bir üye bulunamadı");
                return View(new UserApp());
            }
            
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Edit(UserApp app,string password)
        {
            UserApp user= await UserManagerApp.FindByIdAsync(app.Id);
            if (Helpers.Gecerli(password))
            {
                user.Name = app.Name;
                user.Surname = app.Surname;
                user.Email = app.Email;
                user.UserName = app.UserName;
                user.UyeMi = true;
                user.Sha512Pass = Helpers.GetHashPass(password);
                IdentityResult result = await UserManagerApp.UpdateAsync(user);

                 if (result.Succeeded)
                 {
                    return RedirectToRoute("uyeler");
                 }
                 else
                 { 
                   result.Errors.ToList().ForEach(x => ModelState.AddModelError("", x));
                   return View(user);
                 }
            }else
            {
                return View(user);
            }
           
        }

        //[Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(string id)
        {
            UserApp user = await UserManagerApp.FindByIdAsync(id);
            user.UyeMi = false;
            IdentityResult result = await UserManagerApp.UpdateAsync(user);

            return RedirectToRoute("uyeler");
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (!User.Identity.IsAuthenticated)
            { 
                ViewBag.url = returnUrl;
                return View(new UserLoginModel());
            }
            else { return RedirectToRoute("Home"); }
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserLoginModel user, string returnUrl)
        {

            if (user.Email != null && user.Password != null)
            {
                UserApp logUser = UserManagerApp.FindByEmail(user.Email);
                if (logUser != null && logUser.UyeMi)
                {
                    //UserApp CurrentUser = await UserManagerApp.FindAsync(logUser.UserName, user.Password);
                    bool check;
                    check = logUser.CheckPassword(user.Password);
                    if (check)
                    {
                        //ClaimsIdentity ident = await UserManagerApp.CreateIdentityAsync(logUser, DefaultAuthenticationTypes.ApplicationCookie);
                        //HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties() { IsPersistent = true }, ident);
                        FormsAuthentication.SetAuthCookie(user.Email, true);
                        if (returnUrl != null)
                        { return Redirect(returnUrl); }
                        else
                        {
                            return RedirectToRoute("Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Parola yanlış!");
                        return View(user);
                    }

                    /*if (CurrentUser == null)
                    {
                       
                    }
                    else
                    {
                        ClaimsIdentity ident = await UserManagerApp.CreateIdentityAsync(CurrentUser,
                        DefaultAuthenticationTypes.ApplicationCookie);
                        HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties() { IsPersistent = true },ident);
                        if (returnUrl != null)
                        { return Redirect(returnUrl); }
                        else
                        {
                            return RedirectToRoute("Home");
                        }
                    }*/
                }
                else
                {
                    string email = user.Email;
                    ModelState.AddModelError("", email + " adresi sistemde kayıtlı değil veya silinmiş olabilir!");
                    return View(user);
                }
            }
            else
            {
                ModelState.AddModelError("", "E-mail adresinizi ve parolanızı giriniz!");
                return View(user);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("Home");
        }
    }
}