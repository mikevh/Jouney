using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Journey.Web.App_Start;
using Journey.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Journey.Web.Controllers
{
    [Authorize]
    public class Leaders1Controller : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private ApplicationUserManager UserManager => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

        [HttpGet]
        [Route("api/whoami")]
        public IHttpActionResult WhoAmI() {
            if (User.Identity.IsAuthenticated) {
                string id = User.Identity.GetUserId();
                var rv = new {
                    Id = id,
                    Roles = UserManager.GetRoles(id),
                    User.Identity.Name
                };
                return Ok(rv);
            }
            return Unauthorized();
        }

        public IHttpActionResult GetLeaders() {
            var rv = _db.Leaders.Where(x => !x.IsDeleted).ToDtos();
            return Ok(rv);
        }

        public IHttpActionResult GetLeader(int id) {
            var model = _db.Leaders.Find(id);
            if (model == null) {
                return NotFound();
            }
            var rv = model.ToDto();
            return Ok(rv);
        }

        public IHttpActionResult PutLeader(int id, DTO.Leader vm) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (id != vm.Id) {
                return BadRequest();
            }
            
            var email = _db.Leaders.AsNoTracking().Single(x => x.Id == vm.Id).Email;
            var model = vm.ToModel();
            _db.Entry(model).State = EntityState.Modified;

            var user = UserManager.FindByEmail(email);
            user.UserName = vm.Email;
            user.Email = vm.Email;
            user.LockoutEnabled = false;

            try {
                UserManager.Update(user);
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) {
                if (!LeaderExists(id)) {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult PostLeader(DTO.CreateLeader vm) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var newUser = new ApplicationUser {
                UserName = vm.Email,
                Email = vm.Email,
            };
            var model = new Models.Leader {Email = vm.Email};
            var result = UserManager.Create(newUser, vm.Password ?? "password");
            newUser.LockoutEnabled = false;
            UserManager.Update(newUser);
            _db.Leaders.Add(model);

            if (result.Succeeded) {
                _db.SaveChanges();
            }

            return Ok(model.Id);
        }

        public IHttpActionResult DeleteLeader(int id) {
            var model = _db.Leaders.Find(id);
            if (model == null) {
                return NotFound();
            }

            if (_db.CommunityGroups.Any(x => x.LeaderId == id && !x.IsDeleted))
            {
                throw new InvalidOperationException("This leader leads a community group, reassign group leadership before deleting this leader");
            }

            model.IsDeleted = true;

            var user = UserManager.FindByEmail(model.Email);
            UserManager.Delete(user);

            _db.SaveChanges();
            var rv = model.ToDto();

            return Ok(rv);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LeaderExists(int id) {
            return _db.Leaders.Count(e => e.Id == id) > 0;
        }
    }
}