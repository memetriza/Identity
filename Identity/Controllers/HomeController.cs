using System.Web.Mvc;

namespace Identity.Controllers
{
    public class HomeController : Controller
    {
       /* public UserManagerApp UserManagerApp
        {
            get
            {
                IOwinContext context = HttpContext.GetOwinContext();
                return context.GetUserManager<UserManagerApp>();
            }
        }*/
        // GET: Home

        public ActionResult Index()
        {
            return View();
        }
    }
}