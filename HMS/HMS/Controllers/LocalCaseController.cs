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
            ViewResult NewView = View(db.LocalCases.Where(x => x.isactive.Equals(true)).ToList());
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
            ViewResult NewView = View(db.LocalCases.Where(x => x.isactive.Equals(true)).ToList());
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
            //ViewResult NewView = View(db.LocalCases.ToList());
            ViewResult NewView = View(db.LocalCases.Where(x => x.isactive.Equals(true)).ToList());
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
            ViewResult NewView = View(db.LocalCases.Where(x => x.isactive.Equals(true)).ToList());
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
            if (GlobalVariable.currentRole.Equals("Admin"))
            {
                    myLayoutName = "_Layout_Admin";
            }

            ViewResult myView = View(localCase);
            myView.MasterName = myLayoutName;
            return myView;
        }

        public ActionResult DetailsArzt(int? id)
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
            if (GlobalVariable.currentRole.Equals("Arzt"))
            {
                myLayoutName = "_Layout_Arzt";
            }

            ViewResult myView = View(localCase);
            myView.MasterName = myLayoutName;
            return myView;
        }

        // GET: LocalCase/Create
        public ActionResult Create()
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Admin"))
            {
                    myLayoutName = "_Layout_Admin";
                   
              
            }

            ViewResult myView = View();
            myView.MasterName = myLayoutName;

            //Für Raum:
            ViewBag.Id= new SelectList(db.Rooms.Where(x => x.vacancy != "0").ToList() , "Id", "number");

            //Für User
            
            ViewBag.IdUser = new SelectList(db.Users, "Id", "surname");

            //Für Patient
            var url = Url.RequestContext.RouteData.Values["Id"];
            int id = System.Convert.ToInt32(url);
            ViewBag.Pat = new SelectList(db.Patients.Where(x => x.Id.Equals(id)).ToList(), "Id", "surname");
            return myView;
        }

        public ActionResult CreateArzt()
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Arzt"))
            {
                myLayoutName = "_Layout_Arzt";
            }
            ViewResult NewView = View();
          

            //Für Raum:
            ViewBag.Id = new SelectList(db.Rooms, "Id", "number");

            //Für User
            ViewBag.IdUser = new SelectList(db.Users, "Id", "surname");

            //Für Patient
            var url = Url.RequestContext.RouteData.Values["Id"];
            int id = System.Convert.ToInt32(url);
            ViewBag.Pat = new SelectList(db.Patients.Where(x => x.Id.Equals(id)).ToList(), "Id", "surname");

            NewView.MasterName = myLayoutName;
            return NewView;
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
          
                
                //Mit dem Befehlt kann man einem Case einem Raum zuordnen.
                //Wir müssen also irgendwie noch die id beim Create mitliefern (int? idRoom) und hier dann speichern
                //Analog auch mit Patienten
                //localCase.Patient.Add(db.Patient.Find(idPat));

                localCase.timecreate = DateTime.Now;
                localCase.timemodify = DateTime.Now;

                //
                //Hier wird die Beziehung Raum - Behandlung gespeichert
                //
                string wirbrauchendieid = Request.Form["Id"].ToString();
                int roomId = System.Convert.ToInt32(wirbrauchendieid);
                localCase.Room.Add(db.Rooms.Find(roomId));

                //
                //Hier wird die Beziehung User - Behandlung gespeichert
                //
                string dieidvomuser = Request.Form["IdUser"].ToString();
                int userId = System.Convert.ToInt32(dieidvomuser);
                localCase.User.Add(db.Users.Find(userId));


                //
                //Hier wird die Beziehung Patient - Behandlung gespeichert
                //
                string dieidvompat = Request.Form["Pat"].ToString();
                int patId = System.Convert.ToInt32(dieidvompat);
                localCase.Patient.Add(db.Patients.Find(patId));
                if(localCase.casenr == "Aufnahme")
                {
                    Patient pat = db.Patients.Find(patId);
                    pat.isactive = true;
                    Room room = db.Rooms.Find(roomId);
                    string ausgang = room.vacancy;
                    int vacancyUpdated = System.Convert.ToInt32(ausgang);
                    vacancyUpdated = vacancyUpdated - 1;
                    ausgang = System.Convert.ToString(vacancyUpdated);
                    room.vacancy = ausgang;

                }
                


                ViewBag.Id = new SelectList(db.Rooms, "Id", "number", localCase.Id);
                ViewBag.IdUser = new SelectList(db.Users, "Id", "surname", localCase.Id);
                db.LocalCases.Add(localCase);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            

            return View(localCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateArzt([Bind(Include = "Id,timecreate,timeclosed,casenr,diagnosis,medication,therapy,expectedtime,timemodify,isactive")] LocalCase localCase)
        {
            if (ModelState.IsValid)
            {


                //Mit dem Befehlt kann man einem Case einem Raum zuordnen.
                //Wir müssen also irgendwie noch die id beim Create mitliefern (int? idRoom) und hier dann speichern
                //Analog auch mit Patienten
                //localCase.Patient.Add(db.Patient.Find(idPat));

                localCase.timecreate = DateTime.Now;
                localCase.timemodify = DateTime.Now;

                //
                //Hier wird die Beziehung Raum - Behandlung gespeichert
                //
                string wirbrauchendieid = Request.Form["Id"].ToString();
                int roomId = System.Convert.ToInt32(wirbrauchendieid);
                localCase.Room.Add(db.Rooms.Find(roomId));

                //
                //Hier wird die Beziehung User - Behandlung gespeichert
                //
                string dieidvomuser = Request.Form["IdUser"].ToString();
                int userId = System.Convert.ToInt32(dieidvomuser);
                localCase.User.Add(db.Users.Find(userId));


                //
                //Hier wird die Beziehung Patient - Behandlung gespeichert
                //
                string dieidvompat = Request.Form["Pat"].ToString();
                int patId = System.Convert.ToInt32(dieidvompat);
                localCase.Patient.Add(db.Patients.Find(patId));


                ViewBag.Id = new SelectList(db.Rooms, "Id", "number", localCase.Id);
                ViewBag.IdUser = new SelectList(db.Users, "Id", "surname", localCase.Id);
                db.LocalCases.Add(localCase);

                db.SaveChanges();
                return RedirectToAction("IndexArzt");
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
            if (GlobalVariable.currentRole.Equals("Admin"))
            {
                    myLayoutName = "_Layout_Admin";
            }
            ViewResult myView = View(localCase);
            myView.MasterName = myLayoutName;

            //Für Raum:
            //ViewBag.Id = der Name der Liste
            //db.Rooms = Datenbank, "Id" = Attribut 
           
            ViewBag.IdEditAdmin = null;
            ViewBag.IdEditAdmin = new SelectList(db.Rooms,  "Id", "number");

            //Für User
            ViewBag.IdUserAdmin = null;
            ViewBag.IdUserAdmin = new SelectList(db.Users, "Id", "surname");

            return myView;
        }

        public ActionResult EditArzt(int? id)
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
            if (GlobalVariable.currentRole.Equals("Arzt"))
            {
                myLayoutName = "_Layout_Arzt";
            }

            ViewResult myView = View(localCase);
            myView.MasterName = myLayoutName;

            //Für Raum
            ViewBag.IdEditArzt = null;
            ViewBag.IdEditArzt = new SelectList(db.Rooms, "Id", "number");

            //Für User
            ViewBag.IdUserArzt = null;
            ViewBag.IdUserArzt = new SelectList(db.Users, "Id", "surname");

            return myView;
        }

        // POST: LocalCase/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,timecreate,timeclosed,casenr,diagnosis,medication,therapy,expectedtime,timecreate,timemodify,isactive")] LocalCase localCase)
        {
            if (ModelState.IsValid)
            {
                localCase.timemodify = DateTime.Now;
                //TODO:
                //Räume und User werden beim Edit noch nicht gespeichert
                //bisher keine Lösung gefunden
                //

                var item = db.Entry<LocalCase>(localCase);
                item.State = System.Data.Entity.EntityState.Modified;
                //Raum löschen
                item.Collection(i => i.Room).Load();
                localCase.Room.Clear();
                //User löschen
                item.Collection(i => i.User).Load();
                localCase.User.Clear();

                //
                //Hier wird die Beziehung Raum - Behandlung gespeichert
                //
                string wirbrauchendieid = Request.Form["IdEditAdmin"].ToString();
                int roomId = System.Convert.ToInt32(wirbrauchendieid);
                localCase.Room.Add(db.Rooms.Find(roomId));
                //
                //Hier wird die Beziehung User - Behandlung gespeichert
                //
                string dieidvomuser = Request.Form["IdUserAdmin"].ToString();
                int userId = System.Convert.ToInt32(dieidvomuser);
                localCase.User.Add(db.Users.Find(userId));

                db.Entry(localCase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(localCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditArzt([Bind(Include = "Id,timecreate,timeclosed,casenr,diagnosis,medication,therapy,expectedtime,timecreate,timemodify,isactive")] LocalCase localCase)
        {
            if (ModelState.IsValid)
            {
                localCase.timemodify = DateTime.Now;
                //TODO:
                //Räume und User werden beim Edit noch nicht gespeichert
                //bisher keine Lösung gefunden
                //

                var item = db.Entry<LocalCase>(localCase);
                item.State = System.Data.Entity.EntityState.Modified;
                //Raum löschen
                item.Collection(i => i.Room).Load();
                localCase.Room.Clear();
                //User löschen
                item.Collection(i => i.User).Load();
                localCase.User.Clear();

                //
                //Hier wird die Beziehung Raum - Behandlung gespeichert
                //
                string wirbrauchendieid = Request.Form["IdEditArzt"].ToString();
                int roomId = System.Convert.ToInt32(wirbrauchendieid);
                localCase.Room.Add(db.Rooms.Find(roomId));
                //
                //Hier wird die Beziehung User - Behandlung gespeichert
                //
                string dieidvomuser = Request.Form["IdUserArzt"].ToString();
                int userId = System.Convert.ToInt32(dieidvomuser);
                localCase.User.Add(db.Users.Find(userId));

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
            if (GlobalVariable.currentRole.Equals("Admin"))
            {
                    myLayoutName = "_Layout_Admin";
            }

            ViewResult myView = View(localCase);
            myView.MasterName = myLayoutName;
            return myView;
        }

        public ActionResult DeleteArzt(int? id)
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
            if (GlobalVariable.currentRole.Equals("Arzt"))
            {
                myLayoutName = "_Layout_Arzt";
            }

            ViewResult myView = View(localCase);
            myView.MasterName = myLayoutName;
            return myView;
        }

        // POST: LocalCase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LocalCase localCase = db.LocalCases.Find(id);
            //db.LocalCases.Remove(localCase);
            localCase.isactive = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("DeleteArzt")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedArzt(int id)
        {
            LocalCase localCase = db.LocalCases.Find(id);
            //db.LocalCases.Remove(localCase);
            localCase.isactive = false;
            db.SaveChanges();
            return RedirectToAction("IndexArzt");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Autor: Martin Wichary
        public ActionResult Historie(int? id)
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Admin"))
            {
                myLayoutName = "_Layout_Admin";
            }
            if (id == null)
            {
                ViewResult NewViewErr = View(db.Patients.ToList());
                NewViewErr.MasterName = myLayoutName;
                return NewViewErr;
            }
            Patient pat = db.Patients.Find(id);
            ViewResult NewView = View(pat.LocalCase.ToList());
            NewView.MasterName = myLayoutName;
            return NewView;
        }
        //Autor: Martin Wichary
        public ActionResult HistorieArzt(int? id)
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Arzt"))
            {
                myLayoutName = "_Layout_Arzt";
            }
            if (id == null)
            {
                ViewResult NewViewErr = View(db.Patients.ToList());
                NewViewErr.MasterName = myLayoutName;
                return NewViewErr;
            }
            Patient pat = db.Patients.Find(id);
            ViewResult NewView = View(pat.LocalCase.ToList());
            NewView.MasterName = myLayoutName;
            return NewView;
        }

        //Autor: Martin Wichary
        public ActionResult HistoriePfleger(int? id)
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Schwester"))
            {
                myLayoutName = "_Layout_Schwester";
            }
            if (id == null)
            {
                ViewResult NewViewErr = View(db.Patients.ToList());
                NewViewErr.MasterName = myLayoutName;
                return NewViewErr;
            }
            Patient pat = db.Patients.Find(id);
            ViewResult NewView = View(pat.LocalCase.ToList());
            NewView.MasterName = myLayoutName;
            return NewView;
        }
        public ActionResult HistorieTherapeut(int? id)
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Therapeut"))
            {
                myLayoutName = "_Layout_Therapeut";
            }
            if (id == null)
            {
                ViewResult NewViewErr = View(db.Patients.ToList());
                NewViewErr.MasterName = myLayoutName;
                return NewViewErr;
            }
            Patient pat = db.Patients.Find(id);
            ViewResult NewView = View(pat.LocalCase.ToList());
            NewView.MasterName = myLayoutName;
            return NewView;
        }

        // GET: LocalCase/PatientAufnahme
        public ActionResult PatientAufnahme()
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Admin"))
            {
                myLayoutName = "_Layout_Admin";
            }

            ViewResult myView = View();
            myView.MasterName = myLayoutName;

            //Für Raum:
            ViewBag.Id = new SelectList(db.Rooms.Where(x => x.vacancy != "0" && x.type == "Patientenzimmer").ToList(), "Id", "number");

            //Für User

            ViewBag.IdUser = new SelectList(db.Users, "Id", "surname");

            //Für Patient
            var url = Url.RequestContext.RouteData.Values["Id"];
            int id = System.Convert.ToInt32(url);
            ViewBag.Pat = new SelectList(db.Patients.Where(x => x.Id.Equals(id)).ToList(), "Id", "surname");
            return myView;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PatientAufnahme([Bind(Include = "Id,timecreate,timeclosed,casenr,diagnosis,medication,therapy,expectedtime,timemodify,isactive")] LocalCase localCase)
        {
            if (ModelState.IsValid)
            {
                localCase.timecreate = DateTime.Now;
                localCase.timemodify = DateTime.Now;

                //
                //Hier wird die Beziehung Raum - Behandlung gespeichert
                //
                string wirbrauchendieid = Request.Form["Id"].ToString();
                int roomId = System.Convert.ToInt32(wirbrauchendieid);
                localCase.Room.Add(db.Rooms.Find(roomId));
                //
                //Hier wird die Beziehung User - Behandlung gespeichert
                //
                string dieidvomuser = Request.Form["IdUser"].ToString();
                int userId = System.Convert.ToInt32(dieidvomuser);
                localCase.User.Add(db.Users.Find(userId));
                //
                //Hier wird die Beziehung Patient - Behandlung gespeichert
                //
                string dieidvompat = Request.Form["Pat"].ToString();
                int patId = System.Convert.ToInt32(dieidvompat);
                localCase.Patient.Add(db.Patients.Find(patId));
                if (localCase.casenr == "Aufnahme")
                {
                    Patient pat = db.Patients.Find(patId);
                    pat.isactive = true;
                    Room room = db.Rooms.Find(roomId);
                    string ausgang = room.vacancy;
                    int vacancyUpdated = System.Convert.ToInt32(ausgang);
                    vacancyUpdated = vacancyUpdated - 1;
                    ausgang = System.Convert.ToString(vacancyUpdated);
                    room.vacancy = ausgang;

                }
                ViewBag.Id = new SelectList(db.Rooms, "Id", "number", localCase.Id);
                ViewBag.IdUser = new SelectList(db.Users, "Id", "surname", localCase.Id);
                db.LocalCases.Add(localCase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(localCase);
        }

        // GET: LocalCase/PatientAufnahme
        public ActionResult PatientAufnahmeArzt()
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Admin"))
            {
                myLayoutName = "_Layout_Admin";
            }

            ViewResult myView = View();
            myView.MasterName = myLayoutName;

            //Für Raum:
            ViewBag.Id = new SelectList(db.Rooms.Where(x => x.vacancy != "0" && x.type == "Patientenzimmer").ToList(), "Id", "number");

            //Für User

            ViewBag.IdUser = new SelectList(db.Users, "Id", "surname");

            //Für Patient
            var url = Url.RequestContext.RouteData.Values["Id"];
            int id = System.Convert.ToInt32(url);
            ViewBag.Pat = new SelectList(db.Patients.Where(x => x.Id.Equals(id)).ToList(), "Id", "surname");
            return myView;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PatientAufnahmeArzt([Bind(Include = "Id,timecreate,timeclosed,casenr,diagnosis,medication,therapy,expectedtime,timemodify,isactive")] LocalCase localCase)
        {
            if (ModelState.IsValid)
            {
                localCase.timecreate = DateTime.Now;
                localCase.timemodify = DateTime.Now;

                //
                //Hier wird die Beziehung Raum - Behandlung gespeichert
                //
                string wirbrauchendieid = Request.Form["Id"].ToString();
                int roomId = System.Convert.ToInt32(wirbrauchendieid);
                localCase.Room.Add(db.Rooms.Find(roomId));
                //
                //Hier wird die Beziehung User - Behandlung gespeichert
                //
                string dieidvomuser = Request.Form["IdUser"].ToString();
                int userId = System.Convert.ToInt32(dieidvomuser);
                localCase.User.Add(db.Users.Find(userId));
                //
                //Hier wird die Beziehung Patient - Behandlung gespeichert
                //
                string dieidvompat = Request.Form["Pat"].ToString();
                int patId = System.Convert.ToInt32(dieidvompat);
                localCase.Patient.Add(db.Patients.Find(patId));
                if (localCase.casenr == "Aufnahme")
                {
                    Patient pat = db.Patients.Find(patId);
                    pat.isactive = true;
                    Room room = db.Rooms.Find(roomId);
                    string ausgang = room.vacancy;
                    int vacancyUpdated = System.Convert.ToInt32(ausgang);
                    vacancyUpdated = vacancyUpdated - 1;
                    ausgang = System.Convert.ToString(vacancyUpdated);
                    room.vacancy = ausgang;

                }
                ViewBag.Id = new SelectList(db.Rooms, "Id", "number", localCase.Id);
                ViewBag.IdUser = new SelectList(db.Users, "Id", "surname", localCase.Id);
                db.LocalCases.Add(localCase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(localCase);
        }

        // GET: LocalCase/PatientAufnahme
        public ActionResult PatientAufnahmePflege()
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Admin"))
            {
                myLayoutName = "_Layout_Admin";
            }

            ViewResult myView = View();
            myView.MasterName = myLayoutName;

            //Für Raum:
            ViewBag.Id = new SelectList(db.Rooms.Where(x => x.vacancy != "0" && x.type == "Patientenzimmer").ToList(), "Id", "number");

            //Für User

            ViewBag.IdUser = new SelectList(db.Users, "Id", "surname");

            //Für Patient
            var url = Url.RequestContext.RouteData.Values["Id"];
            int id = System.Convert.ToInt32(url);
            ViewBag.Pat = new SelectList(db.Patients.Where(x => x.Id.Equals(id)).ToList(), "Id", "surname");
            return myView;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PatientAufnahmePflege([Bind(Include = "Id,timecreate,timeclosed,casenr,diagnosis,medication,therapy,expectedtime,timemodify,isactive")] LocalCase localCase)
        {
            if (ModelState.IsValid)
            {
                localCase.timecreate = DateTime.Now;
                localCase.timemodify = DateTime.Now;

                //
                //Hier wird die Beziehung Raum - Behandlung gespeichert
                //
                string wirbrauchendieid = Request.Form["Id"].ToString();
                int roomId = System.Convert.ToInt32(wirbrauchendieid);
                localCase.Room.Add(db.Rooms.Find(roomId));
                //
                //Hier wird die Beziehung User - Behandlung gespeichert
                //
                string dieidvomuser = Request.Form["IdUser"].ToString();
                int userId = System.Convert.ToInt32(dieidvomuser);
                localCase.User.Add(db.Users.Find(userId));
                //
                //Hier wird die Beziehung Patient - Behandlung gespeichert
                //
                string dieidvompat = Request.Form["Pat"].ToString();
                int patId = System.Convert.ToInt32(dieidvompat);
                localCase.Patient.Add(db.Patients.Find(patId));
                if (localCase.casenr == "Aufnahme")
                {
                    Patient pat = db.Patients.Find(patId);
                    pat.isactive = true;
                    Room room = db.Rooms.Find(roomId);
                    string ausgang = room.vacancy;
                    int vacancyUpdated = System.Convert.ToInt32(ausgang);
                    vacancyUpdated = vacancyUpdated - 1;
                    ausgang = System.Convert.ToString(vacancyUpdated);
                    room.vacancy = ausgang;

                }
                ViewBag.Id = new SelectList(db.Rooms, "Id", "number", localCase.Id);
                ViewBag.IdUser = new SelectList(db.Users, "Id", "surname", localCase.Id);
                db.LocalCases.Add(localCase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(localCase);
        }



        //  --------------CK-----------------------

        // Ich denke Ihr habt keine Wahl außer für jeden Action in jedem Controller zu checken wer das darf. !!!!! Mühsam aber müssen wir leider auch.

        public ActionResult Schichten()
        {
            return View();
        }

        public ActionResult SchichtenPfleger()
        {
            return View();
        }
        public ActionResult SchichtenTherapeut()
        {
            return View();
        }

        // GET: LocalCase/GetSchichten
        public JsonResult GetSchichten()
        {

            //Es muss eine Liste erzeugt werden in denen diese Angaben vorliegen. Eine Beschreibung hier "casenr", 
            //ein Startdatum als Datetime hier "timecreate" , eine Enddatum als Datetime hier "timeclosed" und eine Eindeutige ID, hier "Id" des Eintrags.

            //Wie diese Liste erzeugt wird ist egal auch der Typ der Liste ist egal. Alle Änderungen der Namen(casenr,timecreate,timeclosed,Id) können in der 
            //Views /LocalCase/Schichten.cshtml in den Zeilen 31 - 36 angepasst werden. Achtung auch in der Schichten view müssen die Zeilen 61-68 angepasst werden.

            //Nun einfach die Daten zusammensuchen die Ihr anzeigen wollt und voila, werden dann angezeigt.
            //Über dieses Model könnte das schon funktionieren ist aber recht unsauber oder ihr müsst ein neues erstellen, aber das müsst ihr entscheiden.

            // WICHTIG: KONNTE NICHT MIT DER DB TESTEN WEIL DIE BEI MIR NICHT LÄUFT, daher habe ich hier einige Testbeispiele eingefügt. Über db dann anzeigename des termins, 
            //start und ende und eine eindeutige id zusammensuchen. habe bei euch im model leider kein passenderen Controller, Model hierfür gefunden.

            List<LocalCase> termine = new List<LocalCase>();

            termine.Add(new LocalCase()
            {
                casenr = "test",
                timecreate = DateTime.Now,
                timeclosed = DateTime.Now.AddHours(2),
                Id = 1
            });

            termine.Add(new LocalCase()
            {
                casenr = "Adrian",
                timecreate = DateTime.Now.AddHours(3),
                timeclosed = DateTime.Now.AddHours(4),
                Id = 2
            });
            //Mit mit for each die Termine reinballern aber nur für den jeweiligen Arzt
            //Dazu brauchen wir ne globale Variable amkkkkkk
            //LocalCase loc = db.LocalCases.Find(24);
            //termine.Add(new LocalCase()
            //{
            //    casenr = loc.casenr,
            //    timecreate = loc.timecreate,
            //    timeclosed = loc.timeclosed,
            //    Id = loc.Id
            //});
            


            return Json(termine, JsonRequestBehavior.AllowGet);
        }
        //  --------------CK-End----------------------

        public JsonResult GetSchichtenPfleger()
        {
            List<LocalCase> termine = new List<LocalCase>();

            termine.Add(new LocalCase()
            {
                casenr = "test",
                timecreate = DateTime.Now,
                timeclosed = DateTime.Now.AddHours(2),
                Id = 1
            });

            termine.Add(new LocalCase()
            {
                casenr = "Jonas",
                timecreate = DateTime.Now.AddHours(3),
                timeclosed = DateTime.Now.AddHours(4),
                Id = 2
            });




            return Json(termine, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSchichtenTherapeut()
        {
            List<LocalCase> termine = new List<LocalCase>();

            termine.Add(new LocalCase()
            {
                casenr = "test",
                timecreate = DateTime.Now,
                timeclosed = DateTime.Now.AddHours(2),
                Id = 1
            });

            termine.Add(new LocalCase()
            {
                casenr = "Martin",
                timecreate = DateTime.Now.AddHours(3),
                timeclosed = DateTime.Now.AddHours(4),
                Id = 2
            });




            return Json(termine, JsonRequestBehavior.AllowGet);
        }


    }
}
