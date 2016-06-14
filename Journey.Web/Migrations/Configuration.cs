using System.Configuration;
using Journey.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Journey.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Journey.Web.Models.JourneyModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Journey.Web.Models.JourneyModel context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }

    internal sealed class UserConfiguration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public UserConfiguration() {
            AutomaticMigrationsEnabled = true;
        }

        private string adminUsername => ConfigurationManager.AppSettings["adminUsername"] ?? "admin@admin.com";
        private string adminPassword => ConfigurationManager.AppSettings["adminPassword"] ?? "Pass@word1";

        protected override void Seed(ApplicationDbContext context) {
            if (context.Users.Any(u => u.UserName == adminUsername)) {
                return;
            }
            var adminuser = new ApplicationUser {
                UserName = adminUsername,
                Email = adminUsername,
                PasswordHash = new PasswordHasher().HashPassword(adminPassword)
            };
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var adminRole = new IdentityRole("Administrator");
            var activeRole = new IdentityRole("Active");

            roleManager.Create(activeRole);
            roleManager.Create(adminRole);
            userManager.Create(adminuser);
            userManager.AddToRole(adminuser.Id, "Administrator");
            userManager.AddToRole(adminuser.Id, "Active");
        }
    }
}
