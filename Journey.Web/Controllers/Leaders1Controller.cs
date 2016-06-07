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
using Journey.Web.App_Start;
using Journey.Web.Models;

namespace Journey.Web.Controllers
{
    public class Leaders1Controller : ApiController
    {
        private readonly JourneyModel _db = new JourneyModel();

        public IHttpActionResult GetLeaders() {
            var rv = _db.Leaders.ToDtos();
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
            var model = vm.ToModel();
            _db.Entry(model).State = EntityState.Modified;

            try {
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

        public IHttpActionResult PostLeader(DTO.Leader vm) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var model = vm.ToModel();
            _db.Leaders.Add(model);
            _db.SaveChanges();

            return Ok(model.Id);
        }

        public IHttpActionResult DeleteLeader(int id) {
            var model = _db.Leaders.Find(id);
            if (model == null) {
                return NotFound();
            }

            _db.Leaders.Remove(model);
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