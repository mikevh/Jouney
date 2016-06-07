using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Journey.Web.App_Start;
using Journey.Web.Models;

namespace Journey.Web.Controllers
{
    public class CommunityGroups1Controller : ApiController
    {
        private readonly JourneyModel _db = new JourneyModel();

        [Route("api/communitygroups1/latest/{id}")]
        public IHttpActionResult GetLatestMeeting(int id) {
            var query = "SELECT MAX(Date) FROM Meetings WHERE CommunityGroupId = @p0";

            var rv = _db.Database.SqlQuery<DateTime?>(query, id).FirstOrDefault();

            return Ok(rv);
        }

        public IHttpActionResult GetCommunityGroups() {
            var rv = _db.CommunityGroups.ToDtos();
            return Ok(rv);
        }

        public IHttpActionResult GetCommunityGroup(int id) {
            var rv = _db.CommunityGroups.Find(id).ToDto();
            if (rv == null) {
                return NotFound();
            }

            return Ok(rv);
        }

        public IHttpActionResult PutCommunityGroup(int id, DTO.CommunityGroup vm) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (id != vm.Id) {
                return BadRequest();
            }
            var model = vm.ToModel();
            _db.Entry(model).State = EntityState.Modified;

            try {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) {
                if (!CommunityGroupExists(id)) {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult PostCommunityGroup(DTO.CommunityGroup vm) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var model = vm.ToModel();
            _db.CommunityGroups.Add(model);
            _db.SaveChanges();

            return Ok(model.Id);
        }

        public IHttpActionResult DeleteCommunityGroup(int id) {
            var model = _db.CommunityGroups.Find(id);
            if (model == null) {
                return NotFound();
            }

            _db.CommunityGroups.Remove(model);
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

        private bool CommunityGroupExists(int id) {
            return _db.CommunityGroups.Count(e => e.Id == id) > 0;
        }
    }
}