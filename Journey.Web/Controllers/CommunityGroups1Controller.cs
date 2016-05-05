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
    public class CommunityGroups1Controller : ApiController
    {
        private JourneyModel db = new JourneyModel();

        // GET: api/CommunityGroups1
        public IQueryable<CommunityGroup> GetCommunityGroups()
        {
            return db.CommunityGroups;
        }

        // GET: api/CommunityGroups1/5
        [ResponseType(typeof(CommunityGroup))]
        public IHttpActionResult GetCommunityGroup(int id)
        {
            CommunityGroup communityGroup = db.CommunityGroups.Find(id);
            if (communityGroup == null)
            {
                return NotFound();
            }

            return Ok(communityGroup);
        }

        // PUT: api/CommunityGroups1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCommunityGroup(int id, CommunityGroup communityGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != communityGroup.Id)
            {
                return BadRequest();
            }

            db.Entry(communityGroup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommunityGroupExists(id))
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

        // POST: api/CommunityGroups1
        [ResponseType(typeof(CommunityGroup))]
        public IHttpActionResult PostCommunityGroup(CommunityGroup communityGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CommunityGroups.Add(communityGroup);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = communityGroup.Id }, communityGroup);
        }

        // DELETE: api/CommunityGroups1/5
        [ResponseType(typeof(CommunityGroup))]
        public IHttpActionResult DeleteCommunityGroup(int id)
        {
            CommunityGroup communityGroup = db.CommunityGroups.Find(id);
            if (communityGroup == null)
            {
                return NotFound();
            }

            db.CommunityGroups.Remove(communityGroup);
            db.SaveChanges();

            return Ok(communityGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommunityGroupExists(int id)
        {
            return db.CommunityGroups.Count(e => e.Id == id) > 0;
        }
    }
}