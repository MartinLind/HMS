using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Models;

namespace HMS.Controllers
{
    public class LocalCaseController : Controller
    {
        private DBContainer db = new DBContainer();

        // GET: LocalCase
        public ActionResult Index()
        {
            return View(db.LocalCases.ToList());
        }

        // GET: LocalCase/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocalCase localCase = db.LocalCases.Find(id);
            if (localCase == null)
            {
                return HttpNotFound();
            }
            return View(localCase);
        }

        // GET: LocalCase/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocalCase/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,timeopen,timeclosed,casenr,personincharge,diagnosis,medication,therapy,expectedtime,Property1,timecreate,timemodify,isactive")] LocalCase localCase)
        {
            if (ModelState.IsValid)
            {
                db.LocalCases.Add(localCase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(localCase);
        }

        // GET: LocalCase/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocalCase localCase = db.LocalCases.Find(id);
            if (localCase == null)
            {
                return HttpNotFound();
            }
            return View(localCase);
        }

        // POST: LocalCase/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,timeopen,timeclosed,casenr,personincharge,diagnosis,medication,therapy,expectedtime,Property1,timecreate,timemodify,isactive")] LocalCase localCase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(localCase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(localCase);
        }

        // GET: LocalCase/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocalCase localCase = db.LocalCases.Find(id);
            if (localCase == null)
            {
                return HttpNotFound();
            }
            return View(localCase);
        }

        // POST: LocalCase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LocalCase localCase = db.LocalCases.Find(id);
            db.LocalCases.Remove(localCase);
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
