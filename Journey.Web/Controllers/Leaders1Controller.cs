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
    public class Leaders1Controller : ApiController
    {
        private JourneyModel db = new JourneyModel();

        // GET: api/Leaders1
        public IQueryable<Leader> GetLeaders()
        {
            return db.Leaders;
        }

        // GET: api/Leaders1/5
        [ResponseType(typeof(Leader))]
        public IHttpActionResult GetLeader(int id)
        {
            Leader leader = db.Leaders.Find(id);
            if (leader == null)
            {
                return NotFound();
            }

            return Ok(leader);
        }

        // PUT: api/Leaders1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLeader(int id, Leader leader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != leader.Id)
            {
                return BadRequest();
            }

            db.Entry(leader).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Leaders1
        [ResponseType(typeof(Leader))]
        public IHttpActionResult PostLeader(Leader leader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Leaders.Add(leader);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = leader.Id }, leader);
        }

        // DELETE: api/Leaders1/5
        [ResponseType(typeof(Leader))]
        public IHttpActionResult DeleteLeader(int id)
        {
            Leader leader = db.Leaders.Find(id);
            if (leader == null)
            {
                return NotFound();
            }

            db.Leaders.Remove(leader);
            db.SaveChanges();

            return Ok(leader);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LeaderExists(int id)
        {
            return db.Leaders.Count(e => e.Id == id) > 0;
        }
    }
}