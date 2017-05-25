using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;

namespace Identity.Infrastructure
{
    public class UserApp:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte[] Sha512Pass { get; set; }

        public bool UyeMi { get; set; }

        public bool CheckPassword(string Pass)
        {
            byte[] checkP = Helpers.GetHashPass(Pass);
            return checkP.SequenceEqual(Sha512Pass);
        }
    }
}