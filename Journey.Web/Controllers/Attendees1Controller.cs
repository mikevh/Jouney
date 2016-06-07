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
    public class Attendees1Controller : ApiController
    {
        private readonly JourneyModel _db = new JourneyModel();

        public IHttpActionResult GetAttendees() {
            var rv = _db.Attendees.ToDtos();

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

            _db.Attendees.Add(attendeeModel);
            _db.SaveChanges();

            return Ok(attendeeModel.Id);
        }

        public IHttpActionResult DeleteAttendee(int id) {
            Attendee attendee = _db.Attendees.Find(id);
            if (attendee == null) {
                return NotFound();
            }

            _db.Attendees.Remove(attendee);
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