using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Identity.Infrastructure
{
    public class UserAppDbContext:IdentityDbContext<UserApp>
    {
        public UserAppDbContext() : base("IdentityDb")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // Database.SetInitializer(new UserAppDbContextInitializer());
            modelBuilder.Entity<UserApp>().Ignore(c => c.EmailConfirmed)
                                          .Ignore(c => c.AccessFailedCount)
                                          .Ignore(c => c.LockoutEnabled)
                                          .Ignore(c => c.LockoutEndDateUtc)
                                          .Ignore(c => c.TwoFactorEnabled)
                                          .Ignore(c=> c.PhoneNumber)
                                          .Ignore(c=> c.PhoneNumberConfirmed)
                                          .Ignore(c=> c.PasswordHash)
                                          .Ignore(c=>c.SecurityStamp);

            base.OnModelCreating(modelBuilder);
        }
    }
    public class UserAppDbContextInitializer : DropCreateDatabaseIfModelChanges<UserAppDbContext>
    {
        protected override void Seed(UserAppDbContext context)
        {
            base.Seed(context);
        }
    }
    
}