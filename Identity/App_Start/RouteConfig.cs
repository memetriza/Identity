using System.Web.Mvc;
using System.Web.Routing;

namespace Identity
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Home", "", new { controller = "Home", action = "Index" });
            routes.MapRoute("uyeler", "uyeler", new { controller = "Account", action = "Index" });
            routes.MapRoute("uyeEkle", "uye_ekle", new { controller = "Account", action = "Create" });
            routes.MapRoute("uyeDuzenle", "uye_duzenle", new { controller = "Account", action = "Edit" });
            routes.MapRoute("uyeSiz", "uye_sil", new { controller = "Account", action = "Delete" });
            routes.MapRoute("Login", "login", new { controller = "Account", action = "Login" });
            routes.MapRoute("Logout", "_logout", new { controller = "Account", action = "Logout" });
            routes.MapRoute("roller", "rol_listesi", new { controller = "Role", action = "Index" });
            routes.MapRoute("rolEkle", "rol_ekle", new { controller = "Role", action = "Create" });
            routes.MapRoute("rolSil", "rol_sil", new { controller = "Role", action = "Delete" });
            routes.MapRoute("rolYonet", "rol_yonet", new { controller = "Role", action = "Yonetim" });
            routes.MapRoute("uyeOl", "uye_ol", new { controller = "Users", action = "UyeOl" });
        }
    }
}
