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
using Journey.Web.App_Start;
using Journey.Web.Extensions;
using Journey.Web.Models;
using NLog;

namespace Journey.Web.Controllers
{
    [Authorize]
    public class Meetings1Controller : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private readonly ILogger _logger;

        public Meetings1Controller()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public IHttpActionResult GetMeetings()
        {
            var rv = db.Meetings
                .Include(x => x.Attendees)
                .Include(x => x.CommunityGroup)
                .Select(m => new DTO.Meeting
                {
                    Id = m.Id,
                    CommunityGroup = new DTO.CommunityGroup {Name = m.CommunityGroup.Name},
                    Date = m.Date,
                    Attendees = m.Attendees.Select(a => new DTO.Attendee
                    {
                        Id = a.Id,
                        Name = a.Name,
                        IsMember = a.IsMember
                    })
                });

            return Ok(rv);
        }

        public IHttpActionResult GetMeeting(int id) {
            var meeting = db.Meetings.Include(x => x.Attendees).SingleOrDefault(x => x.Id == id);
            if (meeting == null) {
                return NotFound();
            }

            var dto = Mapper.Map<DTO.Meeting>(meeting);

            return Ok(dto);
        }

        public IHttpActionResult PutMeeting(int id, DTO.Meeting meeting_vm) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (id != meeting_vm.Id) {
                return BadRequest();
            }

            _logger.Info("Meeting update: " + meeting_vm.ToJson());

            var origModel = db.Meetings.Include(c => c.Attendees).Single(x => x.Id == id);
            db.Entry(origModel).CurrentValues.SetValues(meeting_vm);

            // remove any attendees no longer in attendee list from orig db obj
            foreach (var oa in origModel.Attendees.ToList()) {
                if (meeting_vm.Attendees.All(a => a.Id != oa.Id)) {
                    _logger.Trace($"deleting attendee {oa.Id} from meeting {meeting_vm.Id}");
                    origModel.Attendees.Remove(oa);
                }
            }

            // add all new attendees
            foreach (var na in meeting_vm.Attendees) {
                if (origModel.Attendees.All(a => a.Id != na.Id)) {
                    if (na.Id > 0) {
                        origModel.Attendees.Add(db.Attendees.FirstOrDefault(x => x.Id == na.Id));
                    }
                    else {
                        origModel.Attendees.Add(Mapper.Map<Attendee>(na));
                    }
                }
            }

            db.SaveChanges();

            return Ok();
        }

        public IHttpActionResult PostMeeting(DTO.Meeting meeting) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var model = Mapper.Map<Meeting>(meeting);
            model.Attendees.Clear();
            // for existing attendees, don't create new entries
            foreach (var a in meeting.Attendees.Where(x => x.Id > 0)) {
                model.Attendees.Add(db.Attendees.FirstOrDefault(x => x.Id == a.Id));
            }

            foreach (var a in meeting.Attendees.Where(x => x.Id == 0)) {
                model.Attendees.Add(a.ToModel());
            }
            db.Meetings.Add(model);
            db.SaveChanges();

            return Ok(model.Id);
        }

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