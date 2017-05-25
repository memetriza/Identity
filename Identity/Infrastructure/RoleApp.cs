using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Identity.Infrastructure
{
    public class RoleApp:IdentityRole
    {
        public RoleApp() : base() { }
        public RoleApp(string name) : base(name) { }

        public static implicit operator RoleApp(UserApp v)
        {
            throw new NotImplementedException();
        }
    }
}