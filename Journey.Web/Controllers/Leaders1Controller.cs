using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Journey.Web.App_Start;
using Journey.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NLog;

namespace Journey.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class Leaders1Controller : ApiController
    {
        private readonly Logger _logger;
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private ApplicationUserManager UserManager => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

        public Leaders1Controller()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

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

        public IHttpActionResult GetLeaders()
        {
            // assumes a single admin role in system, if you have a role, it's the admin role
            var users = UserManager.Users
                .Include(x => x.Roles)
                .ToDictionary(x => x.Email, x => x.Roles.Any());

            var rv = _db.Leaders.Where(x => !x.IsDeleted)
                .OrderBy(x => x.Email)
                .Select(x => new DTO.Leader
                {
                    Id = x.Id,
                    Email = x.Email,
                }).ToList();

            foreach (var u in rv)
            {
                u.IsAdmin = users[u.Email];
            }
            return Ok(rv);
        }

        public IHttpActionResult GetLeader(int id) {
            var model = _db.Leaders.Find(id);
            if (model == null) {
                return NotFound();
            }

            // assumes a single admin role in system, if you have a role, it's the admin role
            var isAdmin = UserManager.FindByEmail(model.Email).Roles;

            var rv = new DTO.Leader
            {
                Id = model.Id,
                Email = model.Email,
                IsAdmin = isAdmin.Any()
            };
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
            if (user == null)
            {
                _logger.Error($"User with email of {email} not found");
                throw new ArgumentException($"User not found {email}");
            }
            if (vm.IsAdmin && !user.Roles.Any())
            {
                UserManager.AddToRole(user.Id, "Administrator");
                _logger.Info($"Added Administrator role to {email}");
            }
            else if (!vm.IsAdmin && user.Roles.Any())
            {
                _logger.Info($"Removed Administrator role from {email}");
                user.Roles.Clear();
            }
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