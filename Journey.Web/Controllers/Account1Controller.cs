using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Journey.Web.Controllers
{
    public class Account1Controller : ApiController
    {
        private ApplicationUserManager UserManager => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

        [HttpGet]
        [Route("api/whoami")]
        public IHttpActionResult WhoAmI()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            try
            {
                string id = User.Identity.GetUserId();
                var roles = UserManager.GetRoles(id);
                var rv = new
                {
                    Id = id,
                    Roles = roles,
                    User.Identity.Name
                };
                return Ok(rv);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
    }
}
