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

        // GET: Patient
        //public ActionResult Index()
        //{
            //return View(db.Patients.ToList());
        //}

        //Option sagt uns wonach gesucht wird. Search ist das Suchwort
        //Können noch beliebig viele weitere Suchparameter einbauen
        //MW
  
        public ActionResult Index(string option, string search)
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Admin"))
            {
                myLayoutName = "_Layout_Admin";
            }

            ViewResult myView = View(db.Patients.ToList());

            //if (option == "Gender")
            //{
            //    myView  = View(db.Patients.Where(x => x.gender.StartsWith(search) || search == null).ToList());
            //}
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

            ViewResult myView = View(db.Patients.ToList());

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

            ViewResult myView = View(db.Patients.ToList());

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

            ViewResult myView = View(db.Patients.ToList());

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
        //[Authorize(Roles = "Admin, Arzt, Pfleger")]
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

            ViewResult myView = View(patient);
            myView.MasterName = myLayoutName;
            return myView;
            //return View(patient);
        }

        // GET: Patient/Create
        //[Authorize(Roles = "Admin, Arzt, Pfleger")]
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

        // POST: Patient/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin, Arzt, Pfleger")]
        public ActionResult Create([Bind(Include = "Id,insuranceID,insurance,prename,surname,phone,email,gender,street,city,zip,dateofbirth,timecreate,timemodify,isactive")] Patient patient)
        {
            SqlConnection connection = null;

            String ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=HMSDB;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

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

        // GET: Patient/Edit/5
        //[Authorize(Roles = "Admin, Arzt, Pfleger")]
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

            ViewResult myView = View(patient);
            myView.MasterName = myLayoutName;
            return myView;

            //return View(patient);
        }

        // POST: Patient/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin, Arzt, Pfleger")]
        public ActionResult Edit([Bind(Include = "Id,insuranceID, insurance,prename,surname,phone,email,gender,street,city,zip,dateofbirth,,timecreate, timemodify,isactive")] Patient patient)
        {
            //
            if (ModelState.IsValid)
            {
                //patient.dateofbirth = DateTime.Now;
                //patient.timecreate = DateTime.Now;
                patient.timemodify = DateTime.Now;
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: Patient/Delete/5
        //[Authorize(Roles = "Admin, Arzt, Pfleger")]
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

            ViewResult myView = View(patient);
            myView.MasterName = myLayoutName;
            return myView;
            //return View(patient);
        }

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin, Arzt, Pfleger")]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
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
