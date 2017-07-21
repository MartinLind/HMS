using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Models;
using System.Data.SqlClient;
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
                case GlobalVariable.Role.Therapeut:
                    myLayoutName = "_Layout_Therapeut";
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

        public ActionResult IndexReinigung(int? id)
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Reinigungspersonal"))
            {
                myLayoutName = "_Layout_Reinigungspersonal";
            }
            //Raum wurde gereinigt
            if (id == null)
            {
                foreach (Room item in db.Rooms)
                {
                    TimeSpan ts = DateTime.Now - item.timemodify;
                    int differenceindays = ts.Days;
                    if (differenceindays >= 2)
                    {
                        item.isactive = true;
                        
                    }
                }
                db.SaveChanges();
                ViewResult myView1 = View(db.Rooms.Where(x => x.isactive.Equals(true)));
                myView1.MasterName = myLayoutName;
                return myView1;
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                ViewResult myView2 = View(db.Rooms.Where(x => x.isactive.Equals(true)));
                myView2.MasterName = myLayoutName;
                return myView2;
            }
            room.isactive = false;
            room.timemodify = DateTime.Now;
            db.SaveChanges();


            ViewResult myView = View(db.Rooms.Where(x => x.isactive.Equals(true)));
            myView.MasterName = myLayoutName;
            return myView;
        }

        public ActionResult IndexPfleger(int? id)
        {
            String myLayoutName = "";

            if (GlobalVariable.currentRole.Equals("Pflegepersonal"))
            {
                myLayoutName = "_Layout_Schwester";
            }
            if (id == null)
            {
                foreach (Room item in db.Rooms)
                {
                    TimeSpan ts = DateTime.Now - item.timemodify;
                    int differenceindays = ts.Days;
                    if (differenceindays >= 2)
                    {
                        item.isactive = true;

                    }
                }
                db.SaveChanges();
                ViewResult myView1 = View(db.Rooms.ToList());
                myView1.MasterName = myLayoutName;
                return myView1;
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                ViewResult myView2 = View(db.Rooms.ToList());
                myView2.MasterName = myLayoutName;
                return myView2;
            }
            room.isactive = true;
            room.timemodify = DateTime.Now;
            db.SaveChanges();

            ViewResult myView = View(db.Rooms.ToList());
            myView.MasterName = myLayoutName;
            return myView;



            //return View(db.Rooms.ToList());
        }

        public ActionResult IndexArzt(int? id)
        {
            String myLayoutName = "";

            if (GlobalVariable.currentRole.Equals("Arzt"))
            {
                myLayoutName = "_Layout_Arzt";
            }
            if (id == null)
            {
                foreach (Room item in db.Rooms)
                {
                    TimeSpan ts = DateTime.Now - item.timemodify;
                    int differenceindays = ts.Days;
                    if (differenceindays >= 2)
                    {
                        item.isactive = true;

                    }
                }
                db.SaveChanges();
                ViewResult myView1 = View(db.Rooms.ToList());
                myView1.MasterName = myLayoutName;
                return myView1;
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                ViewResult myView2 = View(db.Rooms.ToList());
                myView2.MasterName = myLayoutName;
                return myView2;
            }
            room.isactive = true;
            room.timemodify = DateTime.Now;
            db.SaveChanges();

            ViewResult myView = View(db.Rooms.ToList());
            myView.MasterName = myLayoutName;
            return myView;



            //return View(db.Rooms.ToList());
        }

        public ActionResult IndexTherapeut(int? id)
        {
            String myLayoutName = "";

            if (GlobalVariable.currentRole.Equals("Therapeut"))
            {
                myLayoutName = "_Layout_Therapeut";
            }
            if (id == null)
            {
                foreach (Room item in db.Rooms)
                {
                    TimeSpan ts = DateTime.Now - item.timemodify;
                    int differenceindays = ts.Days;
                    if (differenceindays >= 2)
                    {
                        item.isactive = true;

                    }
                }
                db.SaveChanges();
                ViewResult myView1 = View(db.Rooms.ToList());
                myView1.MasterName = myLayoutName;
                return myView1;
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                ViewResult myView2 = View(db.Rooms.ToList());
                myView2.MasterName = myLayoutName;
                return myView2;
            }
            room.isactive = true;
            room.timemodify = DateTime.Now;
            db.SaveChanges();
            ViewResult myView = View(db.Rooms.ToList());
            myView.MasterName = myLayoutName;
            return myView;



            //return View(db.Rooms.ToList());
        }

        // GET: Room/Details/5
        //[Authorize(Roles = "Admin")]
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
                case GlobalVariable.Role.Therapeut:
                    myLayoutName = "_Layout_Therapeut";
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
        //[Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            // Author: Ming
            switch (GlobalVariable.currentRole)
            {
                case GlobalVariable.Role.Admin:
                    Room model = new Room();
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
            //old, and incorrect
            //Autor: David Bismor
            //Room model = new Room();
            //model.timecreate = DateTime.Now;
            //model.timemodify = DateTime.Now;

            //String myLayoutName = "";
            //switch (GlobalVariable.currentRole)
            //{
            //    case GlobalVariable.Role.Admin:
            //        myLayoutName = "_Layout_Admin";
            //        break;
            //    case GlobalVariable.Role.Arzt:
            //        myLayoutName = "_Layout_Arzt";
            //        break;
            //    case GlobalVariable.Role.Schwester:
            //        myLayoutName = "_Layout_Schwester";
            //        break;
            //    case GlobalVariable.Role.Therapeut:
            //        myLayoutName = "_Layout_Therapeut";
            //        break;
            //    default:
            //        myLayoutName = "_Layout_Reinigungspersonal";
            //        break;
            //}

            //ViewResult myView = View(model);
            //myView.MasterName = myLayoutName;
            //return myView;

            //return View(model);
        }

        // POST: Room/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,number,space,vacancy,type,timecreate,timemodify,isactive")] Room room)
        {
            SqlConnection connection = null;

            String ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=HMSDB1;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            connection = new SqlConnection(ConnectionString);

            String SQLString = String.Format("SELECT dbo.ObjectSet_Room.number FROM dbo.ObjectSet_Room WHERE number = '{0}' ", room.number);

            SqlCommand cmd = new SqlCommand(SQLString, connection);

            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            String dbroomnumber = null;

           
            while (reader.Read() == true)
            {
                dbroomnumber = Convert.ToString(reader["number"]);
               
            }
            if (ModelState.IsValid)
            {

                if (System.Convert.ToInt32(room.vacancy) > System.Convert.ToInt32(room.space))
                {
                    ModelState.AddModelError("", "Überprüfen Sie Ihre Eingabe auf Richtigkeit! Es können nicht mehr freie Betten angeboten werden als unser Platzangebot!");
                    return View(room);
                    //throw new Exception("Überprüfen Sie Ihre Eingabe!");
                    //ViewBag.Message = "Überprüfen Sie Ihre Eingabe!";
                    //return View(room);
                }

                //db.Rooms.Add(room);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }


            if (dbroomnumber == null)
            {
                /// success
                ///
                room.isactive = true;
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index"); ;
            }
            else
            {
                ModelState.AddModelError("", "Raumnummer bereits vergeben");
            }

           
            return View(room);
        }

        // GET: Room/Edit/5
        //[Authorize(Roles = "Admin")]
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
                case GlobalVariable.Role.Therapeut:
                    myLayoutName = "_Layout_Therapeut";
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
        //[Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,number,space,vacancy,type,timecreate,timemodify,isactive")] Room room)
        {
            SqlConnection connection = null;

            String ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=HMSDB;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            connection = new SqlConnection(ConnectionString);

            String SQLString = String.Format("SELECT dbo.ObjectSet_Room.number FROM dbo.ObjectSet_Room WHERE number = '{0}' ", room.number);

            SqlCommand cmd = new SqlCommand(SQLString, connection);

            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            String dbroomnumber = null;


            while (reader.Read() == true)
            {
                dbroomnumber = Convert.ToString(reader["number"]);

            }

            if (ModelState.IsValid)
            {
                if (System.Convert.ToInt32(room.vacancy) > System.Convert.ToInt32(room.space))
                {
                    ModelState.AddModelError("", "Überprüfen Sie Ihre Eingabe auf Richtigkeit! Es können nicht mehr freie Betten angeboten werden als unser Platzangebot!");
                    return View(room);
                }
                //room.timemodify = DateTime.Now;
                //db.Entry(room).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }

            if (dbroomnumber == null)
            {
                /// success
                room.timemodify = DateTime.Now;
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Raumnummer bereits vergeben");
            }


            return View(room);
        }

        // GET: Room/Delete/5
        //[Authorize(Roles = "Admin")]
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
                case GlobalVariable.Role.Therapeut:
                    myLayoutName = "_Layout_Therapeut";
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
        //[Authorize(Roles = "Admin")]
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
