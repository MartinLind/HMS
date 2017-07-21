using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Models;
using System.Collections;

namespace HMS.Controllers
{
    public class UserController : Controller
    {
        private DBContainer db = new DBContainer();
        
        // GET: User
        //Autor: David & Yunus
        public ActionResult Index()
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Admin"))
            {
                myLayoutName = "_Layout_Admin";
            }
            ViewResult NewView = View(db.Users.ToList());
            NewView.MasterName = myLayoutName;
            return NewView;
        }

        public ActionResult IndexReinigung()
        {
            String myLayoutName ="";
             if(GlobalVariable.currentRole.Equals("Reinigungspersonal"))
            {
                myLayoutName = "_Layout_Reinigungspersonal";
            }
            ViewResult NewView = View(db.Users.ToList());
            NewView.MasterName = myLayoutName;
            return NewView;
        }

        public ActionResult IndexPfleger()
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Pflegepersonal"))
            {
                myLayoutName = "_Layout_Schwester";
            }
           
           ViewResult NewView = View(db.Users.ToList());
            NewView.MasterName = myLayoutName;
            return NewView;

            //return View(db.Users.ToList());
        }

        public ActionResult IndexArzt()
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Arzt"))
            {
                myLayoutName = "_Layout_Arzt";
            }

            ViewResult NewView = View(db.Users.ToList());
            NewView.MasterName = myLayoutName;
            return NewView;

            //return View(db.Users.ToList());
        }

        public ActionResult IndexTherapeut()
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Therapeut"))
            {
                myLayoutName = "_Layout_Therapeut";
            }

            ViewResult NewView = View(db.Users.ToList());
            NewView.MasterName = myLayoutName;
            return NewView;

            //return View(db.Users.ToList());
        }


        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        
        public ActionResult Create()
        {
            // Author: Ming
            switch (GlobalVariable.currentRole)
            {
                case GlobalVariable.Role.Admin:
                    User model = new User();
                    model.timecreate = DateTime.Now;
                    model.timemodify = DateTime.Now;
                    return View(model);
                    break;
                case GlobalVariable.Role.Arzt:
                    return RedirectToAction("IndexArzt");
                    break;
                case GlobalVariable.Role.Schwester:
                    return RedirectToAction("IndexPfleger");
                    break;
                case GlobalVariable.Role.Reinigungspersonal:
                    return RedirectToAction("IndexReinigung");
                    break;
                case GlobalVariable.Role.Therapeut:
                    return RedirectToAction("IndexTherapeut");
                    break;
                case GlobalVariable.Role.Unknown:
                    //return RedirectToAction("_View/Home/Index");
                default:
                    return RedirectToAction("Index", "Home");
                    break;
            }


            /// Old, and incorrect
            ////Autor: David Bismor

            //User model = new User();
            //model.timecreate = DateTime.Now;
            //model.timemodify = DateTime.Now;
           
            //return View(model);
        }

        // POST: User/Create
        // Autor: David Bismor
        // Automatisches setzes der Rechte - in den Views nur auf display:none gesetzt
        // Keine manuelle Zuweisung der Rechte mehr nötig, Platzeinsparung in der View!!
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,username,password,rolename,accessright1,accessright2,accessright3,accessright4,accessright5,prename,surname,phone,email,gender,street,city,zip,dateofbirth,timecreate,timemodify,isactive")] User user)
        {
            if (ModelState.IsValid)
            {
                string value = user.rolename;
                if (value.Equals("Admin")){user.accessright1 = true; }
               else if (value.Equals("Arzt")) { user.accessright2 = true; }
                else if (value.Equals("Pflegepersonal")) { user.accessright3 = true; }
                else if (value.Equals("Reinigungspersonal")) { user.accessright4 = true; }
                else if (value.Equals("Therapeut")) { user.accessright5 = true; }
                user.isactive = true;
                
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(user);
        }

        // GET: User/Edit/5
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,username, password,rolename,accessright1,accessright2,accessright3,accessright4,accessright5,prename,surname,phone,email,gender,street,city,zip,dateofbirth,timecreate,timemodify,isactive")] User user)
        {
            if (ModelState.IsValid)
            {
                string value = user.rolename;
                if (value.Equals("Admin")) { user.accessright1 = true; user.accessright2 = false; user.accessright3 = false;user.accessright4 = false; user.accessright5 = false; }
                else if (value.Equals("Arzt")) { user.accessright2 = true; user.accessright1 = false; user.accessright3 = false; user.accessright4 = false; user.accessright5 = false; }
                else if (value.Equals("Pflegepersonal")) { user.accessright3 = true; user.accessright2 = false; user.accessright1 = false; user.accessright4 = false; user.accessright5 = false; }
                else if (value.Equals("Reinigungspersonal")) { user.accessright4 = true; user.accessright2 = false; user.accessright3 = false; user.accessright1 = false; user.accessright5 = false; }
                else if (value.Equals("Therapeut")) { user.accessright5 = true; user.accessright2 = false; user.accessright3 = false; user.accessright4 = false; user.accessright1 = false; }
                user.timemodify = DateTime.Now;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Delete/5
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
