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
using Journey.Web.Models;

namespace Journey.Web.Controllers
{
    public class Meetings1Controller : ApiController
    {
        private JourneyModel db = new JourneyModel();

        public IQueryable<Meeting> GetMeetings() {
            return db.Meetings;
        }

        [ResponseType(typeof(Meeting))]
        public IHttpActionResult GetMeeting(int id) {
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null) {
                return NotFound();
            }

            return Ok(meeting);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutMeeting(int id, Meeting meeting) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (id != meeting.Id) {
                return BadRequest();
            }

            var orig = db.Meetings.Include(c => c.Attendees).Single(x => x.Id == id);
            db.Entry(orig).CurrentValues.SetValues(meeting);

            foreach (var oa in orig.Attendees.ToList()) {
                if (!meeting.Attendees.Any(a => a.Id == oa.Id)) {
                    orig.Attendees.Remove(oa);
                }
            }

            foreach (var na in meeting.Attendees) {
                if (!orig.Attendees.Any(a => a.Id == na.Id)) {
                    db.Attendees.Attach(na);
                    orig.Attendees.Add(na);
                }
            }

            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Meeting))]
        public IHttpActionResult PostMeeting(Meeting meeting) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            db.Meetings.Add(meeting);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = meeting.Id }, meeting);
        }

        [ResponseType(typeof(Meeting))]
        public IHttpActionResult DeleteMeeting(int id) {
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null) {
                return NotFound();
            }

            db.Meetings.Remove(meeting);
            db.SaveChanges();

            return Ok(meeting);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}