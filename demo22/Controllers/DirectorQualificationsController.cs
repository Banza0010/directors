using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using demo22.Models;

namespace demo22.Controllers
{
    public class DirectorQualificationsController : Controller
    {
        private DirectorsDBContext db = new DirectorsDBContext();

        // GET: DirectorQualifications
        public ActionResult Index()
        {
            var directorQualifications = db.DirectorQualifications.Include(d => d.DirectorName1).Include(d => d.Qualification);
            return View(directorQualifications.ToList());
        }

        // GET: DirectorQualifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DirectorQualification directorQualification = db.DirectorQualifications.Find(id);
            if (directorQualification == null)
            {
                return HttpNotFound();
            }
            return View(directorQualification);
        }

        // GET: DirectorQualifications/Create
        public ActionResult Create()
        {
            ViewBag.DirectorID = new SelectList(db.DirectorName1, "DirectorID", "Surname");
            ViewBag.QualificationID = new SelectList(db.Qualifications, "ID", "Qualification1");
            return View();
        }

        // POST: DirectorQualifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DirectorID,QualificationID")] DirectorQualification directorQualification)
        {
            if (ModelState.IsValid)
            {
                db.DirectorQualifications.Add(directorQualification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DirectorID = new SelectList(db.DirectorName1, "DirectorID", "Surname", directorQualification.DirectorID);
            ViewBag.QualificationID = new SelectList(db.Qualifications, "ID", "Qualification1", directorQualification.QualificationID);
            return View(directorQualification);
        }

        // GET: DirectorQualifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DirectorQualification directorQualification = db.DirectorQualifications.Find(id);
            if (directorQualification == null)
            {
                return HttpNotFound();
            }
            ViewBag.DirectorID = new SelectList(db.DirectorName1, "DirectorID", "Surname", directorQualification.DirectorID);
            ViewBag.QualificationID = new SelectList(db.Qualifications, "ID", "Qualification1", directorQualification.QualificationID);
            return View(directorQualification);
        }

        // POST: DirectorQualifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DirectorID,QualificationID")] DirectorQualification directorQualification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(directorQualification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DirectorID = new SelectList(db.DirectorName1, "DirectorID", "Surname", directorQualification.DirectorID);
            ViewBag.QualificationID = new SelectList(db.Qualifications, "ID", "Qualification1", directorQualification.QualificationID);
            return View(directorQualification);
        }

        // GET: DirectorQualifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DirectorQualification directorQualification = db.DirectorQualifications.Find(id);
            if (directorQualification == null)
            {
                return HttpNotFound();
            }
            return View(directorQualification);
        }

        // POST: DirectorQualifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DirectorQualification directorQualification = db.DirectorQualifications.Find(id);
            db.DirectorQualifications.Remove(directorQualification);
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
