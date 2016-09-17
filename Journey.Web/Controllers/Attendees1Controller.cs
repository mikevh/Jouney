using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using Journey.Web.App_Start;
using Journey.Web.Models;

namespace Journey.Web.Controllers
{
    [Authorize]
    public class Attendees1Controller : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public IHttpActionResult GetAttendees() {
            var rv = _db.Attendees
                .Include(x => x.CommunityGroup)
                .Where(x => !x.IsDeleted)
                .ToDtos();

            return Ok(rv);
        }

        public IHttpActionResult GetAttendee(int id) {
            Attendee attendee = _db.Attendees.Find(id);
            if (attendee == null) {
                return NotFound();
            }
            var rv = attendee.ToDto();

            return Ok(rv);
        }

        public IHttpActionResult PutAttendee(int id, DTO.Attendee attendee) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (id != attendee.Id) {
                return BadRequest();
            }

            var attendeeModel = attendee.ToModel();

            attendeeModel.Name = attendeeModel.Name.Trim();
            attendeeModel.IsDeleted = false;

            if (attendeeModel.CommunityGroupId == 0)
            {
                attendeeModel.CommunityGroupId = null;
            }
            _db.Entry(attendeeModel).State = EntityState.Modified;

            try {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) {
                if (!AttendeeExists(id)) {
                    return NotFound();
                }
                throw;
            }

            return Ok();
        }

        public IHttpActionResult PostAttendee(DTO.Attendee attendee) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var attendeeModel = attendee.ToModel();
            attendeeModel.Name = attendeeModel.Name.Trim();
            if (attendeeModel.CommunityGroupId == 0)
            {
                attendeeModel.CommunityGroupId = null;
            }
            _db.Attendees.Add(attendeeModel);
            _db.SaveChanges();

            return Ok(attendeeModel.Id);
        }

        public IHttpActionResult DeleteAttendee(int id) {
            Attendee attendee = _db.Attendees.Find(id);
            if (attendee == null) {
                return NotFound();
            }

            attendee.CommunityGroupId = null;
            attendee.IsDeleted = true;

            _db.SaveChanges();
            var rv = attendee.ToDto();

            return Ok(rv);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AttendeeExists(int id) {
            return _db.Attendees.Count(e => e.Id == id) > 0;
        }
    }
}