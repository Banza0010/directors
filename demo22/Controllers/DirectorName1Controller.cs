using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using demo22.Models;
using System.ComponentModel.DataAnnotations;

namespace demo22.Controllers
{
    public class DirectorName1Controller : Controller
    {
        private DirectorsDBContext db = new DirectorsDBContext();

        public ActionResult Index(string searchBy, string search)
        {


            if (searchBy == "Other")
            {
                return View(db.DirectorName1.Where(x => x.Gender == "None").ToList());
            }
            else
            {
                return View(db.DirectorName1.Where(x => x.Surname.StartsWith(search)).ToList());
            }

        }
       // return View(db.DirectorName1.Where(x => x.FirstName.StartsWith(search) || search == null).ToList());
    
       //  GET: DirectorName1

        // GET: DirectorName1/Details/5
        public ActionResult Details(int? id)
        {
            int directorId = id ?? 0;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DirectorName1 directorName1 = db.DirectorName1.Find(id);

            var ldir = db.DirectorQualifications.Where(x => x.DirectorID.Equals(directorId)).Select(x => x.Qualification).ToList();
            string qualList = "<ul>";
            foreach (var item in ldir)
            {
                qualList += $"<li>{item.Description}</li>";
            }
            qualList += "</ul>";
            ViewBag.Qualifactions = qualList;

            if (directorName1 == null)
            {
                return HttpNotFound();
            }
            return View(directorName1);
        }

        // GET: DirectorName1/Create
        public ActionResult Create()
        {
            ViewBag.Grouping = new SelectList(db.Groupings, "ID", "GroupType");
            return View();
        }

        // POST: DirectorName1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "DirectorID,Surname,Initials,FirstName,Title,YearOfBirth,Gender,Grouping,Profile,UpdateDate")] DirectorName1 directorName1)
        {
           // System.Threading.Thread.Sleep(2000);

            if (ModelState.IsValid)
            {
                directorName1.UpdateDate = DateTime.Now;
                db.DirectorName1.Add(directorName1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Grouping = new SelectList(db.Groupings, "ID", "GroupType", directorName1.Grouping);
            return View(directorName1);
        }

        public ActionResult AddQualification(int id = 0)
        {
            //DirectorsDBContext db = new DirectorsDBContext();
            //DirectorQualification directorQualification = db.DirectorQualifications.FirstOrDefault(x => x.ID.Equals(id));

            return null;//ActionResult();

        }


        public ActionResult DeleteQualification(int id = 0)
        {
            DirectorsDBContext db = new DirectorsDBContext();
            int directorId = 0;
            DirectorQualification directorQualification = db.DirectorQualifications.FirstOrDefault(x => x.ID.Equals(id));
            if (directorQualification != null)
            {
                directorId = directorQualification.DirectorID;
                db.DirectorQualifications.Remove(directorQualification);
                db.SaveChanges();

            }
            return Redirect($"/DirectorName1/Edit/{directorId}");
        }

        // GET: DirectorName1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DirectorName1 directorName1 = db.DirectorName1.Find(id);

            ////fetch and edit
            int directorId = id ?? 0;
         
            var ldir = db.DirectorQualifications.Where(x => x.DirectorID.Equals(directorId)).ToList();
            string qualList = "<table width=\"100%\">";
            foreach (var item in ldir)
            {
                qualList += "<tr>";
                qualList += $"<td width=\"80%\">{item.Qualification.Description}</td><td><a href=\"/DirectorName1/DeleteQualification/{item.ID}\">Remove</a></td>";              
                qualList += "</tr>";
            }
            qualList += "</table>";
            qualList += $"<a href=\"/DirectorName1/AddQualification/{0}\">Add</a>";
            ViewBag.Qualifactions = qualList;


            if (directorName1 == null)
            {
                return HttpNotFound();
            }
            ViewBag.Grouping = new SelectList(db.Groupings, "ID", "GroupType", directorName1.Grouping);
            return View(directorName1);
        }

        // POST: DirectorName1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DirectorID,Surname,Initials,FirstName,Title,YearOfBirth,Gender,Grouping,Profile,UpdateDate")] DirectorName1 directorName1)
        {
            System.Threading.Thread.Sleep(2000);

            if (ModelState.IsValid)
            {
                directorName1.UpdateDate = DateTime.Now;
                db.Entry(directorName1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Grouping = new SelectList(db.Groupings, "ID", "GroupType", directorName1.Grouping);
            return View(directorName1);
        }

        // GET: DirectorName1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DirectorName1 directorName1 = db.DirectorName1.Find(id);
            if (directorName1 == null)
            {
                return HttpNotFound();
            }
            return View(directorName1);
        }

        // POST: DirectorName1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DirectorName1 directorName1 = db.DirectorName1.Find(id);
            db.DirectorName1.Remove(directorName1);
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

        //public ActionResult getQualifications(int id)
        //{
        //    List<Qualification> qq = db.Qualifications.ToList();
        //    List<DirectorQualification> dirQ = db.DirectorQualifications.ToList();


        //    return View(qq);
        //}

        public class DirQ
        {
            [Required(ErrorMessage = "Name is Requirde")]
            public DirectorName1 dirObj { get; set; }
            public DirectorQualification DQObj { get; set; }
        }
    }
}
