using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Journey.Web.App_Start;
using Journey.Web.DTO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Journey.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Journey.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class Account1Controller : ApiController
    {
        public Account1Controller() {
            
        }

        public Account1Controller(ApplicationUserManager userManager, RoleManager<IdentityRole> roleManager ) {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public IHttpActionResult GetUsers() {
            var users = UserManager.Users.ToDtos();
            users.ForEach(x => x.Roles = UserManager.GetRoles(x.Id).ToList());

            return Ok(users);
        }

        [Route("api/account1/{id}")]
        public IHttpActionResult PutUser(string id, DTO.JourneyUser user) {
            return Ok();
        }

        [HttpGet]
        [Route("api/account1/roles")]
        public IHttpActionResult GetRoles() {
            var roles = RoleManager.Roles.Select(x => x.Name);
            return Ok(roles);
        }

        private RoleManager<IdentityRole> _roleManager;

        public RoleManager<IdentityRole> RoleManager {
            get { return _roleManager ?? new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext())); }
            private set { _roleManager = value; }
        }  

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager {
            get { return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }
    }
}