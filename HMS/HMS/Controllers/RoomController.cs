﻿using System;
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
    public class RoomController : Controller
    {
        private DBContainer db = new DBContainer();

        // GET: Room
        public ActionResult Index()
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

            ViewResult myView = View(db.Rooms.ToList());
            myView.MasterName = myLayoutName;
            return myView;



            //return View(db.Rooms.ToList());
        }

        public ActionResult IndexReinigung()
        {
            String myLayoutName = "";
            
            if (GlobalVariable.currentRole.Equals("Reinigungspersonal"))
            {
                myLayoutName = "_Layout_Reinigungspersonal";
            }
            ViewResult myView = View(db.Rooms.Where(x => x.space == x.vacancy ));
            //ViewResult myView = View(db.Rooms.ToList());
            myView.MasterName = myLayoutName;
            return myView;



            //return View(db.Rooms.ToList());
        }

        // GET: Room/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
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

            ViewResult myView = View(room);
            myView.MasterName = myLayoutName;
            return myView;

            //return View(room);
        }

        // GET: Room/Create
        public ActionResult Create()
        {
            //Autor: David Bismor
            Room model = new Room();
            model.timecreate = DateTime.Now;
            model.timemodify = DateTime.Now;

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

            ViewResult myView = View(model);
            myView.MasterName = myLayoutName;
            return myView;

            //return View(model);
        }

        // POST: Room/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,number,space,vacancy,type,timecreate,timemodify,isactive")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(room);
        }

        // GET: Room/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
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

            ViewResult myView = View(room);
            myView.MasterName = myLayoutName;
            return myView;

            //return View(room);
        }

        // POST: Room/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,number,space,vacancy,type,timecreate,timemodify,isactive")] Room room)
        {
            if (ModelState.IsValid)
            {
                room.timemodify = DateTime.Now;
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(room);
        }

        // GET: Room/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
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

            ViewResult myView = View(room);
            myView.MasterName = myLayoutName;
            return myView;
            //return View(room);
        }

        // POST: Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
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
