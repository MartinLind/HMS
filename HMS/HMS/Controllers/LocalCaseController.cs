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
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Admin"))
            {
                myLayoutName = "_Layout_Admin";
            }
            ViewResult NewView = View(db.LocalCases.ToList());
            NewView.MasterName = myLayoutName;
            return NewView;
        }
        public ActionResult IndexPfleger()
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Schwester"))
            {
                myLayoutName = "_Layout_Schwester";
            }
            ViewResult NewView = View(db.LocalCases.ToList());
            NewView.MasterName = myLayoutName;
            return NewView;
        }

        public ActionResult IndexArzt()
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Arzt"))
            {
                myLayoutName = "_Layout_Arzt";
            }
            ViewResult NewView = View(db.LocalCases.ToList());
            NewView.MasterName = myLayoutName;
            return NewView;
        }

        public ActionResult IndexTherapeut()
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Therapeut"))
            {
                myLayoutName = "_Layout_Therapeut";
            }
            ViewResult NewView = View(db.LocalCases.ToList());
            NewView.MasterName = myLayoutName;
            return NewView;
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
            String myLayoutName = "";
            switch (GlobalVariable.currentRole)
            {
                case GlobalVariable.Role.Admin:
                    myLayoutName = "_Layout_Admin";
                    break;
                case GlobalVariable.Role.Arzt:
                    myLayoutName = "_Layout_Arzt";
                    break;
                case GlobalVariable.Role.Schwester:
                    myLayoutName = "_Layout_Schwester";
                    break;
                default:
                    myLayoutName = "_Layout_Reinigungspersonal";
                    break;
            }

            ViewResult myView = View(localCase);
            myView.MasterName = myLayoutName;
            return myView;
            //return View(localCase);
        }

        // GET: LocalCase/Create
        public ActionResult Create()
        {
            String myLayoutName = "";
            switch (GlobalVariable.currentRole)
            {
                case GlobalVariable.Role.Admin:
                    myLayoutName = "_Layout_Admin";
                    break;
                case GlobalVariable.Role.Arzt:
                    myLayoutName = "_Layout_Arzt";
                    break;
                case GlobalVariable.Role.Schwester:
                    myLayoutName = "_Layout_Schwester";
                    break;
                default:
                    myLayoutName = "_Layout_Reinigungspersonal";
                    break;
            }

            ViewResult myView = View();
            myView.MasterName = myLayoutName;
            return myView;
            //return View();
        }

        // POST: LocalCase/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,timecreate,timeclosed,casenr,diagnosis,medication,therapy,expectedtime,timemodify,isactive")] LocalCase localCase)
        {
            if (ModelState.IsValid)
            {

                //localCase.Room.Add(db.Rooms.Find(idRoom));
                //Mit dem Befehlt kann man einem Case einem Raum zuordnen.
                //Wir müssen also irgendwie noch die id beim Create mitliefern (int? idRoom) und hier dann speichern
                //Analog auch mit Patienten
                //localCase.Patient.Add(db.Patient.Find(idPat));

                localCase.timecreate = DateTime.Now;
                //localCase.timeclosed = DateTime.Now;
                localCase.timemodify = DateTime.Now;
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
            String myLayoutName = "";
            switch (GlobalVariable.currentRole)
            {
                case GlobalVariable.Role.Admin:
                    myLayoutName = "_Layout_Admin";
                    break;
                case GlobalVariable.Role.Arzt:
                    myLayoutName = "_Layout_Arzt";
                    break;
                case GlobalVariable.Role.Schwester:
                    myLayoutName = "_Layout_Schwester";
                    break;
                default:
                    myLayoutName = "_Layout_Reinigungspersonal";
                    break;
            }

            ViewResult myView = View(localCase);
            myView.MasterName = myLayoutName;
            return myView;
            //return View(localCase);
        }

        // POST: LocalCase/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,timecreate,timeclosed,casenr,diagnosis,medication,therapy,expectedtime,timecreate,timemodify,isactive")] LocalCase localCase)
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
            String myLayoutName = "";
            switch (GlobalVariable.currentRole)
            {
                case GlobalVariable.Role.Admin:
                    myLayoutName = "_Layout_Admin";
                    break;
                case GlobalVariable.Role.Arzt:
                    myLayoutName = "_Layout_Arzt";
                    break;
                case GlobalVariable.Role.Schwester:
                    myLayoutName = "_Layout_Schwester";
                    break;
                default:
                    myLayoutName = "_Layout_Reinigungspersonal";
                    break;
            }

            ViewResult myView = View(localCase);
            myView.MasterName = myLayoutName;
            return myView;
            //return View(localCase);
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
