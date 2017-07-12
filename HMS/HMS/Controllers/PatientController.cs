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
    public class PatientController : Controller
    {
    private DBContainer db = new DBContainer();

   
        public ActionResult Index(string option, string search)
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Admin"))
            {
                myLayoutName = "_Layout_Admin";
            }

            ViewResult myView = View(db.Patients.Where(x => x.isactive.Equals(true)).ToList());
            if (option == "Name")
            {
                myView = View(db.Patients.Where(x => x.surname.StartsWith(search) || search == null).ToList());
            }
            else if (option == "Geburtsdatum")
            {
                try
                {
                    DateTime searchDate = Convert.ToDateTime(search);
                    myView = View(db.Patients.Where(x => x.dateofbirth.Equals(searchDate) || search == null).ToList());
                }
                catch (System.FormatException)
                {
                    myView = View(db.Patients.ToList());
                }
            }
            myView.MasterName = myLayoutName;
            return myView;
        }


        public ActionResult IndexTherapeut(string option, string search)
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Therapeut"))
            {
                myLayoutName = "_Layout_Therapeut";
            }

            ViewResult myView = View(db.Patients.Where(x => x.isactive.Equals(true)).ToList());

            if (option == "Gender")
            {
                myView = View(db.Patients.Where(x => x.gender.StartsWith(search) || search == null).ToList());
            }
            else if (option == "Name")
            {
                myView = View(db.Patients.Where(x => x.surname.StartsWith(search) || search == null).ToList());
            }
            else if (option == "Geburtsdatum")
            {
                try
                {
                    DateTime searchDate = Convert.ToDateTime(search);
                    myView = View(db.Patients.Where(x => x.dateofbirth.Equals(searchDate) || search == null).ToList());
                }
                catch (System.FormatException)
                {
                    myView = View(db.Patients.ToList());
                }

            }

            myView.MasterName = myLayoutName;

            return myView;
        }

        public ActionResult IndexPfleger(string option, string search)
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Pflegepersonal"))
            {
                myLayoutName = "_Layout_Schwester";
            }

            ViewResult myView = View(db.Patients.Where(x => x.isactive.Equals(true)).ToList());

            if (option == "Gender")
            {
                myView = View(db.Patients.Where(x => x.gender.StartsWith(search) || search == null).ToList());
            }
            else if (option == "Name")
            {
                myView = View(db.Patients.Where(x => x.surname.StartsWith(search) || search == null).ToList());
            }
            else if (option == "Geburtsdatum")
            {
                try
                {
                    DateTime searchDate = Convert.ToDateTime(search);
                    myView = View(db.Patients.Where(x => x.dateofbirth.Equals(searchDate) || search == null).ToList());
                }
                catch (System.FormatException)
                {
                    myView = View(db.Patients.ToList());
                }

            }

            myView.MasterName = myLayoutName;

            return myView;
        }

        public ActionResult IndexArzt(string option, string search)
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Arzt"))
            {
                myLayoutName = "_Layout_Arzt";
            }

            ViewResult myView = View(db.Patients.Where(x => x.isactive.Equals(true)).ToList());

            if (option == "Gender")
            {
                myView = View(db.Patients.Where(x => x.gender.StartsWith(search) || search == null).ToList());
            }
            else if (option == "Name")
            {
                myView = View(db.Patients.Where(x => x.surname.StartsWith(search) || search == null).ToList());
            }
            else if (option == "Geburtsdatum")
            {
                try
                {
                    DateTime searchDate = Convert.ToDateTime(search);
                    myView = View(db.Patients.Where(x => x.dateofbirth.Equals(searchDate) || search == null).ToList());
                }
                catch (System.FormatException)
                {
                    myView = View(db.Patients.ToList());
                }

            }

            myView.MasterName = myLayoutName;

            return myView;
        }

        // GET: Patient/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Admin"))
            {

                myLayoutName = "_Layout_Admin";

            }

            ViewResult myView = View(patient);
            myView.MasterName = myLayoutName;
            return myView;
        }

        public ActionResult DetailsArzt(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Arzt"))
            {

                myLayoutName = "_Layout_Arzt";

            }

            ViewResult myView = View(patient);
            myView.MasterName = myLayoutName;
            return myView;
        }

        public ActionResult DetailsPfleger(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Pfleger"))
            {

                myLayoutName = "_Layout_Pfleger";

            }

            ViewResult myView = View(patient);
            myView.MasterName = myLayoutName;
            return myView;
        }
        public ActionResult DetailsTherapeut(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Therapeut"))
            {

                myLayoutName = "_Layout_Therapeut";

            }

            ViewResult myView = View(patient);
            myView.MasterName = myLayoutName;
            return myView;
        }

        // GET: Patient/Create

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
        }

        public ActionResult CreateArzt()
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Arzt"))
            {
              
              myLayoutName = "_Layout_Arzt";
                        
            }

            ViewResult myView = View();
            myView.MasterName = myLayoutName;
            return myView;
        }

        public ActionResult CreatePfleger()
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Pfleger"))
            {

                myLayoutName = "_Layout_Pfleger";

            }

            ViewResult myView = View();
            myView.MasterName = myLayoutName;
            return myView;
        }


        // POST: Patient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,insuranceID,insurance,prename,surname,phone,email,gender,street,city,zip,dateofbirth,timecreate,timemodify,isactive")] Patient patient)
        {
            SqlConnection connection = null;

            String ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=HMSDB1;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            connection = new SqlConnection(ConnectionString);

            String SQLString = String.Format("SELECT dbo.ObjectSet_Patient.insuranceID FROM dbo.ObjectSet_Patient WHERE insuranceID = '{0}' ", patient.insuranceID);

            SqlCommand cmd = new SqlCommand(SQLString, connection);

            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            String dbroomnumber = null;


            while (reader.Read() == true)
            {
                dbroomnumber = Convert.ToString(reader["insuranceID"]);

            }
          

            if (dbroomnumber == null)
            {
                /// success
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index"); ;
            }
            else
            {
                ModelState.AddModelError("", "Patient bereits vergeben");
            }


            if (ModelState.IsValid)
            {
                patient.timecreate = DateTime.Now;
                patient.timemodify = DateTime.Now;
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateArzt([Bind(Include = "Id,insuranceID,insurance,prename,surname,phone,email,gender,street,city,zip,dateofbirth,timecreate,timemodify,isactive")] Patient patient)
        {
            SqlConnection connection = null;

            String ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=HMSDB1;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            connection = new SqlConnection(ConnectionString);

            String SQLString = String.Format("SELECT dbo.ObjectSet_Patient.insuranceID FROM dbo.ObjectSet_Patient WHERE insuranceID = '{0}' ", patient.insuranceID);

            SqlCommand cmd = new SqlCommand(SQLString, connection);

            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            String dbroomnumber = null;


            while (reader.Read() == true)
            {
                dbroomnumber = Convert.ToString(reader["insuranceID"]);

            }


            if (dbroomnumber == null)
            {
                /// success
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("IndexArzt"); ;
            }
            else
            {
                ModelState.AddModelError("", "Patient bereits vergeben");
            }


            if (ModelState.IsValid)
            {
                patient.timecreate = DateTime.Now;
                patient.timemodify = DateTime.Now;
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("IndexArzt");
            }

            return View(patient);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePfleger([Bind(Include = "Id,insuranceID,insurance,prename,surname,phone,email,gender,street,city,zip,dateofbirth,timecreate,timemodify,isactive")] Patient patient)
        {
            SqlConnection connection = null;

            String ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=HMSDB1;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            connection = new SqlConnection(ConnectionString);

            String SQLString = String.Format("SELECT dbo.ObjectSet_Patient.insuranceID FROM dbo.ObjectSet_Patient WHERE insuranceID = '{0}' ", patient.insuranceID);

            SqlCommand cmd = new SqlCommand(SQLString, connection);

            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            String dbroomnumber = null;


            while (reader.Read() == true)
            {
                dbroomnumber = Convert.ToString(reader["insuranceID"]);

            }


            if (dbroomnumber == null)
            {
                /// success
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("IndexPfleger"); ;
            }
            else
            {
                ModelState.AddModelError("", "Patient bereits vergeben");
            }


            if (ModelState.IsValid)
            {
                patient.timecreate = DateTime.Now;
                patient.timemodify = DateTime.Now;
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("IndexPfleger");
            }

            return View(patient);
        }

        // GET: Patient/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Admin"))
            {

                myLayoutName = "_Layout_Admin";

            }

            ViewResult myView = View(patient);
            myView.MasterName = myLayoutName;
            return myView;
        }

        public ActionResult EditArzt(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Arzt"))
            {

                myLayoutName = "_Layout_Arzt";

            }

            ViewResult myView = View(patient);
            myView.MasterName = myLayoutName;
            return myView;
        }

        public ActionResult EditPfleger(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Pfleger"))
            {

                myLayoutName = "_Layout_Pfleger";

            }

            ViewResult myView = View(patient);
            myView.MasterName = myLayoutName;
            return myView;
        }

        // POST: Patient/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,insuranceID, insurance,prename,surname,phone,email,gender,street,city,zip,dateofbirth,,timecreate, timemodify,isactive")] Patient patient)
        {
            //
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Patient");
            }
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditArzt([Bind(Include = "Id,insuranceID, insurance,prename,surname,phone,email,gender,street,city,zip,dateofbirth,,timecreate, timemodify,isactive")] Patient patient)
        {
            //
            if (ModelState.IsValid)
            {
                patient.timemodify = DateTime.Now;
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexArzt","Patient");
            }
            return View(patient);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPfleger([Bind(Include = "Id,insuranceID, insurance,prename,surname,phone,email,gender,street,city,zip,dateofbirth,,timecreate, timemodify,isactive")] Patient patient)
        {
            //
            if (ModelState.IsValid)
            {
                patient.timemodify = DateTime.Now;
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexPfleger", "Patient");
            }
            return View(patient);
        }


        // GET: Patient/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Admin"))
            {

                myLayoutName = "_Layout_Admin";

            }

            ViewResult myView = View(patient);
            myView.MasterName = myLayoutName;
            return myView;
        }

        public ActionResult DeleteArzt(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Arzt"))
            {

                myLayoutName = "_Layout_Arzt";

            }

            ViewResult myView = View(patient);
            myView.MasterName = myLayoutName;
            return myView;
        }

        public ActionResult DeletePfleger(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Pfleger"))
            {

                myLayoutName = "_Layout_Pfleger";

            }

            ViewResult myView = View(patient);
            myView.MasterName = myLayoutName;
            return myView;
        }

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("DeleteArzt")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedArzt(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("IndexArzt");
        }

        [HttpPost, ActionName("DeletePfleger")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedPfleger(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("IndexPfleger");
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
