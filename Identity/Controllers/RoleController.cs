using Identity.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Identity.Controllers
{
    public class RoleController : Controller
    {
        public UserManagerApp UserManagerApp
        {
            get
            {
                IOwinContext context = HttpContext.GetOwinContext();
                return context.GetUserManager<UserManagerApp>();
            }
        }
        public RoleAppManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<RoleAppManager>();
            }
        }
        // GET: Role
        //[Authorize(Roles ="admin")]
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }
        //[Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View(new RoleApp());
        }
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(RoleApp role)
        {
            if (role.Name != null)
            { 
                RoleManager.Create(role);
                return RedirectToRoute("roller");
            }else
            {
                ModelState.AddModelError("", "Rol girmediniz");
                return View(role);
            }
        }

        //[Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(string id)
        {
            RoleApp role = await RoleManager.FindByIdAsync(id);
            IdentityResult result = await RoleManager.DeleteAsync(role);
            return RedirectToRoute("roller");
        }
        //[Authorize(Roles = "admin")]
        public ActionResult Yonetim()
        {
            return View(UserManagerApp.Users);
        }
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Yonetim(IEnumerable<string> RoleNames,string userId)
        {
            IEnumerable<string> rolenames = RoleNames ?? new List<string>();
            IEnumerable<string> selectedrolenames = rolenames;
            IEnumerable<string> unselectedroleNames = Helpers.GetRoles().Select(x => x.Name).Except(rolenames);

            foreach (var srol in selectedrolenames.ToList())
            {
                if (!UserManagerApp.IsInRole(userId,srol))
                {
                    UserManagerApp.AddToRole(userId, srol);
                }
            }
            foreach (string urol in unselectedroleNames.ToList())
            {
                if (UserManagerApp.IsInRole(userId, urol))
                {
                    UserManagerApp.RemoveFromRole(userId, urol);
                }
            }

            return RedirectToRoute("roller");
        }
    }
}