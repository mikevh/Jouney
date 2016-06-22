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

            roleManager.Create(adminRole);
            userManager.Create(adminuser);
            userManager.AddToRole(adminuser.Id, "Administrator");
        }
    }
}
