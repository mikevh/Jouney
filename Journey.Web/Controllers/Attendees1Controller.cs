using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Journey.Web.Models;

namespace Journey.Web.Controllers
{
    public class Attendees1Controller : ApiController
    {
        private readonly JourneyModel _db = new JourneyModel();

        // GET: api/Attendees1
        public IHttpActionResult GetAttendees() {
            var attendees = _db.Attendees.ToList();

            var rv = attendees.Select(Mapper.Map<DTO.Attendee>);

            return Ok(rv);
        }

        // GET: api/Attendees1/5
        public IHttpActionResult GetAttendee(int id) {
            Attendee attendee = _db.Attendees.Find(id);
            if (attendee == null) {
                return NotFound();
            }

            return Ok(Mapper.Map<DTO.Attendee>(attendee));
        }

        // PUT: api/Attendees1/5
        public IHttpActionResult PutAttendee(int id, DTO.Attendee attendee) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (id != attendee.Id) {
                return BadRequest();
            }

            var attendeeModel = Mapper.Map<Attendee>(attendee);
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

        // POST: api/Attendees1
        public IHttpActionResult PostAttendee(DTO.Attendee attendee) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var attendeeModel = Mapper.Map<Attendee>(attendee);

            _db.Attendees.Add(attendeeModel);
            _db.SaveChanges();

            return Ok(attendeeModel.Id);
        }

        // DELETE: api/Attendees1/5
        public IHttpActionResult DeleteAttendee(int id) {
            Attendee attendee = _db.Attendees.Find(id);
            if (attendee == null) {
                return NotFound();
            }

            _db.Attendees.Remove(attendee);
            _db.SaveChanges();

            return Ok(Mapper.Map<DTO.Attendee>(attendee));
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