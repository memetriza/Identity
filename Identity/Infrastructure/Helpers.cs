using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Identity.Infrastructure
{
    public static class Helpers
    {
        public static string GetUserName(string id)
        {
            return HttpContext.Current.GetOwinContext().GetUserManager<UserManagerApp>().FindById(id).UserName;
        }

        public static IEnumerable<RoleApp> GetRoles()
        {
            return HttpContext.Current.GetOwinContext().GetUserManager<RoleAppManager>().Roles;
        }

        public static string GetUsersName(string userName)
        {
            return HttpContext.Current.GetOwinContext().GetUserManager<UserManagerApp>().FindByName(userName).Name;
        }
        public static string GetUsersName2(string eMail)
        {
            return HttpContext.Current.GetOwinContext().GetUserManager<UserManagerApp>().FindByEmail(eMail).Name;
        }
        public static byte[] GetHashPass(string password)
        {
            byte[] hashed;
            var pass = Encoding.UTF8.GetBytes(password);
            using (SHA512 shaM=new SHA512Managed())
            {
                hashed = shaM.ComputeHash(pass);
            }
            return hashed;
        }
        public static bool Gecerli (string pass)
        {
            if(pass.Any(c => char.IsDigit(c))&
               pass.Any(c => char.IsUpper(c))&
               pass.Any(c => char.IsLower(c))&
               pass !="")
            {
                return true;
            }
            else { return false; }
               
        }
    }
}