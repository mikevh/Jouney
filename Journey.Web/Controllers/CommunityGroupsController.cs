using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Journey.Web.Models;

namespace Journey.Web.Controllers
{
    public class CommunityGroupsController : Controller
    {
        private JourneyModel db = new JourneyModel();

        // GET: CommunityGroups
        public ActionResult Index()
        {
            return View(db.CommunityGroups.ToList());
        }

        // GET: CommunityGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommunityGroup communityGroup = db.CommunityGroups.Find(id);
            if (communityGroup == null)
            {
                return HttpNotFound();
            }
            return View(communityGroup);
        }

        // GET: CommunityGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommunityGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] CommunityGroup communityGroup)
        {
            if (ModelState.IsValid)
            {
                db.CommunityGroups.Add(communityGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(communityGroup);
        }

        // GET: CommunityGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommunityGroup communityGroup = db.CommunityGroups.Find(id);
            if (communityGroup == null)
            {
                return HttpNotFound();
            }
            return View(communityGroup);
        }

        // POST: CommunityGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] CommunityGroup communityGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(communityGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(communityGroup);
        }

        // GET: CommunityGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommunityGroup communityGroup = db.CommunityGroups.Find(id);
            if (communityGroup == null)
            {
                return HttpNotFound();
            }
            return View(communityGroup);
        }

        // POST: CommunityGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommunityGroup communityGroup = db.CommunityGroups.Find(id);
            db.CommunityGroups.Remove(communityGroup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
