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
    public class ObjectController : Controller
    {
        private DBContainer db = new DBContainer();

        // GET: Object
        public ActionResult Index()
        {
            return View(db.ObjectSet.ToList());
        }

        // GET: Object/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Object @object = db.ObjectSet.Find(id);
            if (@object == null)
            {
                return HttpNotFound();
            }
            return View(@object);
        }

        // GET: Object/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Object/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,timecreate,timemodify,isactive")] Models.Object @object)
        {
            if (ModelState.IsValid)
            {
                db.ObjectSet.Add(@object);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@object);
        }

        // GET: Object/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Object @object = db.ObjectSet.Find(id);
            if (@object == null)
            {
                return HttpNotFound();
            }
            return View(@object);
        }

        // POST: Object/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,timecreate,timemodify,isactive")] Models.Object @object)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@object).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@object);
        }

        // GET: Object/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Object @object = db.ObjectSet.Find(id);
            if (@object == null)
            {
                return HttpNotFound();
            }
            return View(@object);
        }

        // POST: Object/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.Object @object = db.ObjectSet.Find(id);
            db.ObjectSet.Remove(@object);
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
