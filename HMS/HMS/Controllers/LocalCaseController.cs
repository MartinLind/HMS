using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using HMS.Models;

namespace HMS.Controllers
{
    public class LocalCaseController : Controller
    {
        private DBContainer db = new DBContainer();
      //  object raumnummer;
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
            switch (GlobalVariable.currentRole)
            {
                case GlobalVariable.Role.Admin:
                    return RedirectToAction("Index");
                    break;
                case GlobalVariable.Role.Arzt:
                    LocalCase model = new LocalCase();
                    model.timecreate = DateTime.Now;
                    model.timemodify = DateTime.Now;
                    ViewBag.Id = new SelectList(db.Rooms.Where(x => x.vacancy != "0").ToList(), "Id", "number");

                    //Für User

                    ViewBag.IdUser = new SelectList(db.Users, "Id", "surname");

                    //Für Patient
                    var url = Url.RequestContext.RouteData.Values["Id"];
                    int id = System.Convert.ToInt32(url);
                    ViewBag.Pat = new SelectList(db.Patients.Where(x => x.Id.Equals(id)).ToList(), "Id", "surname");
                   
        
                    return View(model);
                  
                    break;
                case GlobalVariable.Role.Schwester:
                    return RedirectToAction("IndexPfleger");
                    break;
                case GlobalVariable.Role.Reinigungspersonal:
                    return RedirectToAction("Index","Room");
                    break;
                case GlobalVariable.Role.Therapeut:
                    return RedirectToAction("IndexTherapeut");
                    break;
                //case GlobalVariable.Role.Unknown:
                default:
                    return RedirectToAction("Index"/*, "Home"*/);
                    break;
            }
            //    String myLayoutName = "";
            //    if (GlobalVariable.currentRole.Equals("Admin"))
            //    {
            //            myLayoutName = "_Layout_Admin";


            //    }

            //    ViewResult myView = View();
            //    myView.MasterName = myLayoutName;

            //Für Raum:
            //ViewBag.Id= new SelectList(db.Rooms.Where(x => x.vacancy != "0").ToList() , "Id", "number");

            ////Für User
            
            //ViewBag.IdUser = new SelectList(db.Users, "Id", "surname");

            ////Für Patient
            //var url = Url.RequestContext.RouteData.Values["Id"];
            //int id = System.Convert.ToInt32(url);
            //ViewBag.Pat = new SelectList(db.Patients.Where(x => x.Id.Equals(id)).ToList(), "Id", "surname");
            //return myView;
        }

        public ActionResult CreateArzt()
        {

            switch (GlobalVariable.currentRole)
            {
                case GlobalVariable.Role.Admin:
                    return RedirectToAction("Index");
                    break;
                case GlobalVariable.Role.Arzt:
                    LocalCase loc = new LocalCase();
                    loc.timecreate = DateTime.Now;
                    loc.timemodify = DateTime.Now;
                    ViewResult myView = View(loc);
                    //myView.MasterName = myLayoutName;

                    //Für Uhrzeit:
                    var TimeHHListStart = new List<SelectListItem>();
                    TimeHHListStart.Add(new SelectListItem { Text = "00", Value = "00" });
                    TimeHHListStart.Add(new SelectListItem { Text = "01", Value = "01" });
                    TimeHHListStart.Add(new SelectListItem { Text = "02", Value = "02" });
                    TimeHHListStart.Add(new SelectListItem { Text = "03", Value = "03" });
                    TimeHHListStart.Add(new SelectListItem { Text = "04", Value = "04" });
                    TimeHHListStart.Add(new SelectListItem { Text = "05", Value = "05" });
                    TimeHHListStart.Add(new SelectListItem { Text = "06", Value = "06" });
                    TimeHHListStart.Add(new SelectListItem { Text = "07", Value = "07" });
                    TimeHHListStart.Add(new SelectListItem { Text = "08", Value = "08" });
                    TimeHHListStart.Add(new SelectListItem { Text = "09", Value = "09" });
                    TimeHHListStart.Add(new SelectListItem { Text = "10", Value = "10" });
                    TimeHHListStart.Add(new SelectListItem { Text = "11", Value = "11" });
                    TimeHHListStart.Add(new SelectListItem { Text = "12", Value = "12" });
                    TimeHHListStart.Add(new SelectListItem { Text = "13", Value = "13" });
                    TimeHHListStart.Add(new SelectListItem { Text = "14", Value = "14" });
                    TimeHHListStart.Add(new SelectListItem { Text = "15", Value = "15" });
                    TimeHHListStart.Add(new SelectListItem { Text = "16", Value = "16" });
                    TimeHHListStart.Add(new SelectListItem { Text = "17", Value = "17" });
                    TimeHHListStart.Add(new SelectListItem { Text = "19", Value = "18" });
                    TimeHHListStart.Add(new SelectListItem { Text = "20", Value = "19" });
                    TimeHHListStart.Add(new SelectListItem { Text = "21", Value = "20" });
                    TimeHHListStart.Add(new SelectListItem { Text = "22", Value = "22" });
                    TimeHHListStart.Add(new SelectListItem { Text = "23", Value = "23" });

                    ViewBag.UhrHHStart = TimeHHListStart;

                    var TimeMMListStart = new List<SelectListItem>();
                    TimeMMListStart.Add(new SelectListItem { Text = "00", Value = "00" });
                    TimeMMListStart.Add(new SelectListItem { Text = "01", Value = "01" });
                    TimeMMListStart.Add(new SelectListItem { Text = "02", Value = "02" });
                    TimeMMListStart.Add(new SelectListItem { Text = "03", Value = "03" });
                    TimeMMListStart.Add(new SelectListItem { Text = "04", Value = "04" });
                    TimeMMListStart.Add(new SelectListItem { Text = "05", Value = "05" });
                    TimeMMListStart.Add(new SelectListItem { Text = "06", Value = "06" });
                    TimeMMListStart.Add(new SelectListItem { Text = "07", Value = "07" });
                    TimeMMListStart.Add(new SelectListItem { Text = "08", Value = "08" });
                    TimeMMListStart.Add(new SelectListItem { Text = "09", Value = "09" });
                    TimeMMListStart.Add(new SelectListItem { Text = "10", Value = "10" });
                    TimeMMListStart.Add(new SelectListItem { Text = "11", Value = "11" });
                    TimeMMListStart.Add(new SelectListItem { Text = "12", Value = "12" });
                    TimeMMListStart.Add(new SelectListItem { Text = "13", Value = "13" });
                    TimeMMListStart.Add(new SelectListItem { Text = "14", Value = "14" });
                    TimeMMListStart.Add(new SelectListItem { Text = "15", Value = "15" });
                    TimeMMListStart.Add(new SelectListItem { Text = "16", Value = "16" });
                    TimeMMListStart.Add(new SelectListItem { Text = "17", Value = "17" });
                    TimeMMListStart.Add(new SelectListItem { Text = "18", Value = "18" });
                    TimeMMListStart.Add(new SelectListItem { Text = "19", Value = "19" });
                    TimeMMListStart.Add(new SelectListItem { Text = "20", Value = "20" });
                    TimeMMListStart.Add(new SelectListItem { Text = "21", Value = "21" });
                    TimeMMListStart.Add(new SelectListItem { Text = "22", Value = "22" });
                    TimeMMListStart.Add(new SelectListItem { Text = "23", Value = "23" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "24", Value = "24" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "25", Value = "25" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "26", Value = "26" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "27", Value = "27" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "28", Value = "28" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "29", Value = "29" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "30", Value = "30" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "31", Value = "31" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "32", Value = "32" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "33", Value = "33" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "34", Value = "34" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "35", Value = "35" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "36", Value = "36" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "37", Value = "37" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "38", Value = "38" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "39", Value = "39" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "40", Value = "40" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "41", Value = "41" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "42", Value = "42" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "43", Value = "43" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "44", Value = "44" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "45", Value = "45" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "46", Value = "46" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "47", Value = "47" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "48", Value = "48" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "49", Value = "49" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "50", Value = "50" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "51", Value = "51" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "52", Value = "52" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "53", Value = "53" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "54", Value = "54" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "55", Value = "55" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "56", Value = "56" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "57", Value = "57" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "58", Value = "58" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "59", Value = "59" });

                    ViewBag.UhrMMStart = TimeMMListStart;
                    //Ende

                    var TimeHHListEnde = new List<SelectListItem>();
                    TimeHHListEnde.Add(new SelectListItem { Text = "00", Value = "00" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "01", Value = "01" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "02", Value = "02" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "03", Value = "03" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "04", Value = "04" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "05", Value = "05" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "06", Value = "06" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "07", Value = "07" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "08", Value = "08" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "09", Value = "09" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "10", Value = "10" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "11", Value = "11" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "12", Value = "12" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "13", Value = "13" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "14", Value = "14" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "15", Value = "15" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "16", Value = "16" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "17", Value = "17" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "18", Value = "18" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "19", Value = "19" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "20", Value = "20" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "21", Value = "21" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "22", Value = "22" });
                    TimeHHListEnde.Add(new SelectListItem { Text = "23", Value = "23" });

                    ViewBag.UhrHHEnde = TimeHHListEnde;

                    var TimeMMListEnde = new List<SelectListItem>();
                    TimeMMListEnde.Add(new SelectListItem { Text = "00", Value = "00" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "01", Value = "01" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "02", Value = "02" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "03", Value = "03" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "04", Value = "04" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "05", Value = "05" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "06", Value = "06" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "07", Value = "07" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "08", Value = "08" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "09", Value = "09" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "10", Value = "10" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "11", Value = "11" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "12", Value = "12" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "13", Value = "13" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "14", Value = "14" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "15", Value = "15" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "16", Value = "16" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "17", Value = "17" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "18", Value = "18" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "19", Value = "19" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "20", Value = "20" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "21", Value = "21" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "22", Value = "22" });
                    TimeMMListEnde.Add(new SelectListItem { Text = "23", Value = "23" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "24", Value = "24" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "25", Value = "25" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "26", Value = "26" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "27", Value = "27" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "28", Value = "28" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "29", Value = "29" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "30", Value = "30" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "31", Value = "31" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "32", Value = "32" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "33", Value = "33" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "34", Value = "34" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "35", Value = "35" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "36", Value = "36" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "37", Value = "37" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "38", Value = "38" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "39", Value = "39" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "40", Value = "40" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "41", Value = "41" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "42", Value = "42" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "43", Value = "43" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "44", Value = "44" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "45", Value = "45" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "46", Value = "46" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "47", Value = "47" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "48", Value = "48" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "49", Value = "49" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "50", Value = "50" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "51", Value = "51" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "52", Value = "52" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "53", Value = "53" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "54", Value = "54" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "55", Value = "55" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "56", Value = "56" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "57", Value = "57" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "58", Value = "58" });
                    TimeMMListEnde.Add(new SelectListItem() { Text = "59", Value = "59" });

                    ViewBag.UhrMMEnde = TimeMMListEnde;



                    //Für Raum:
                    ViewBag.Id = new SelectList(db.Rooms.Where(x => x.vacancy != "0").ToList(), "Id", "number");

                    //Für User
                    ViewBag.IdUser = new SelectList(db.Users, "Id", "surname");

                    //Für Patient
                    var url = Url.RequestContext.RouteData.Values["Id"];
                    int id = System.Convert.ToInt32(url);
                    ViewBag.Pat = new SelectList(db.Patients.Where(x => x.Id.Equals(id)).ToList(), "Id", "surname");
                    return myView;
                    break;
                case GlobalVariable.Role.Schwester:
                    return RedirectToAction("IndexPfleger");
                    break;
                case GlobalVariable.Role.Reinigungspersonal:
                    return RedirectToAction("Index", "Room");
                    break;
                case GlobalVariable.Role.Therapeut:
                    return RedirectToAction("IndexTherapeut");
                    break;
                //case GlobalVariable.Role.Unknown:
                default:
                    return RedirectToAction("Index"/*, "Home"*/);
                    break;
            }
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

                string stunde = Request.Form["UhrHHStart"].ToString();
                int std = System.Convert.ToInt32(stunde);
                string minute = Request.Form["UhrMMStart"].ToString();
                int min = System.Convert.ToInt32(minute);


                System.DateTime start = new System.DateTime(localCase.timecreate.Year, localCase.timecreate.Month,
                    localCase.timecreate.Day, std, min, 0);
                localCase.timecreate = start;

                string stunde2 = Request.Form["UhrHHEnde"].ToString();
                int std2 = System.Convert.ToInt32(stunde2);
                string minute2 = Request.Form["UhrMMEnde"].ToString();
                int min2 = System.Convert.ToInt32(minute2);


                System.DateTime ende = new System.DateTime(localCase.timeclosed.Year, localCase.timeclosed.Month,
                    localCase.timeclosed.Day, std2, min2, 0);

                localCase.timeclosed = ende;
                localCase.isactive = true;
                //localCase.diagnosis = "offen";
                //localCase.medication = "offen";
                //localCase.therapy = "offen";
                //localCase.expectedtime = "offen";
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
                string stunde = Request.Form["UhrHHStart"].ToString();
                int std = System.Convert.ToInt32(stunde);
                string minute = Request.Form["UhrMMStart"].ToString();
                int min = System.Convert.ToInt32(minute);


                System.DateTime start = new System.DateTime(localCase.timecreate.Year, localCase.timecreate.Month,
                    localCase.timecreate.Day, std, min, 0);
                localCase.timecreate = start;

                string stunde2 = Request.Form["UhrHHEnde"].ToString();
                int std2 = System.Convert.ToInt32(stunde2);
                string minute2 = Request.Form["UhrMMEnde"].ToString();
                int min2 = System.Convert.ToInt32(minute2);


                System.DateTime ende = new System.DateTime(localCase.timeclosed.Year, localCase.timeclosed.Month,
                    localCase.timeclosed.Day, std2, min2, 0);

                localCase.timeclosed = ende;
                localCase.isactive = true;
                //localCase.diagnosis = "offen";
                //localCase.medication = "offen";
                //localCase.therapy = "offen";
                //localCase.expectedtime = "offen";
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

            //A try to get the saved Value and set it as selectedValue in the new List.. wont work..
            // Autor DB
            //SqlConnection connection = null;
            //SqlConnection connection2 = null;

            //String ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=HMSDB1;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            //connection = new SqlConnection(ConnectionString);
            //connection2 = new SqlConnection(ConnectionString);

            //String SQLString = String.Format("SELECT dbo.ObjectSet_Room.number FROM dbo.ObjectSet_Room INNER JOIN dbo.LocalCaseRoom ON dbo.ObjectSet_Room.Id = dbo.LocalCaseRoom.Room_Id INNER JOIN dbo.ObjectSet_LocalCase ON dbo.LocalCaseRoom.LocalCase_Id = dbo.ObjectSet_LocalCase.Id", id);
            //SqlCommand cmd = new SqlCommand(SQLString, connection);
            //connection.Open();

            //String SQLString2 = String.Format("SELECT dbo.ObjectSet_Person.surname FROM dbo.ObjectSet_Person INNER JOIN dbo.ObjectSet_User ON dbo.ObjectSet_Person.Id = dbo.ObjectSet_User.Id INNER JOIN dbo.LocalCaseUser ON dbo.ObjectSet_Person.Id = dbo.LocalCaseUser.User_Id WHERE  id = '{0}'", id);
            //SqlCommand cmd2 = new SqlCommand(SQLString2, connection2);
            //connection2.Open();


            //SqlDataReader reader = cmd.ExecuteReader();

            //while (reader.Read() == true)
            //{
            //     Raum = reader["number"];

            //}

            //Für Raum:
            //ViewBag.Id = der Name der Liste
            //db.Rooms = Datenbank, "Id" = Attribut 

            // ViewBag.IdEditAdmin = null;
            //LocalCase loc = db.LocalCases.Find(id);
            
            //foreach (Room room in loc.Room)
            //{
            //    raumnummer = Convert.ToString(room.number);
            //}

            ViewBag.IdEditAdmin = new SelectList(db.Rooms, "Id", "number");
            
            //Für User
           // ViewBag.IdUserAdmin = null;
            ViewBag.IdUserAdmin = new SelectList(db.Users, "Id", "surname");



             ViewResult myView = View(localCase);
             myView.MasterName = myLayoutName;

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
                return RedirectToAction("IndexArzt");
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
            LocalCase loc = new LocalCase();
            loc.timecreate = DateTime.Now;
            loc.timemodify = DateTime.Now;
            ViewResult myView = View(loc);
            myView.MasterName = myLayoutName;

            //Für Uhrzeit:
            var TimeHHListStart = new List<SelectListItem>();
            TimeHHListStart.Add(new SelectListItem { Text = "00", Value = "00" });
            TimeHHListStart.Add(new SelectListItem { Text = "01", Value = "01" });
            TimeHHListStart.Add(new SelectListItem { Text = "02", Value = "02" });
            TimeHHListStart.Add(new SelectListItem { Text = "03", Value = "03" });
            TimeHHListStart.Add(new SelectListItem { Text = "04", Value = "04" });
            TimeHHListStart.Add(new SelectListItem { Text = "05", Value = "05" });
            TimeHHListStart.Add(new SelectListItem { Text = "06", Value = "06" });
            TimeHHListStart.Add(new SelectListItem { Text = "07", Value = "07" });
            TimeHHListStart.Add(new SelectListItem { Text = "08", Value = "08" });
            TimeHHListStart.Add(new SelectListItem { Text = "09", Value = "09" });
            TimeHHListStart.Add(new SelectListItem { Text = "10", Value = "10" });
            TimeHHListStart.Add(new SelectListItem { Text = "11", Value = "11" });
            TimeHHListStart.Add(new SelectListItem { Text = "12", Value = "12" });
            TimeHHListStart.Add(new SelectListItem { Text = "13", Value = "13" });
            TimeHHListStart.Add(new SelectListItem { Text = "14", Value = "14" });
            TimeHHListStart.Add(new SelectListItem { Text = "15", Value = "15" });
            TimeHHListStart.Add(new SelectListItem { Text = "16", Value = "16" });
            TimeHHListStart.Add(new SelectListItem { Text = "17", Value = "17" });
            TimeHHListStart.Add(new SelectListItem { Text = "18", Value = "18" });
            TimeHHListStart.Add(new SelectListItem { Text = "19", Value = "19" });
            TimeHHListStart.Add(new SelectListItem { Text = "20", Value = "20" });
            TimeHHListStart.Add(new SelectListItem { Text = "21", Value = "21" });
            TimeHHListStart.Add(new SelectListItem { Text = "22", Value = "22" });
            TimeHHListStart.Add(new SelectListItem { Text = "23", Value = "23" });

            ViewBag.UhrHHStart = TimeHHListStart;

            var TimeMMListStart = new List<SelectListItem>();
            TimeMMListStart.Add(new SelectListItem { Text = "00", Value = "00" });
            TimeMMListStart.Add(new SelectListItem { Text = "01", Value = "01" });
            TimeMMListStart.Add(new SelectListItem { Text = "02", Value = "02" });
            TimeMMListStart.Add(new SelectListItem { Text = "03", Value = "03" });
            TimeMMListStart.Add(new SelectListItem { Text = "04", Value = "04" });
            TimeMMListStart.Add(new SelectListItem { Text = "05", Value = "05" });
            TimeMMListStart.Add(new SelectListItem { Text = "06", Value = "06" });
            TimeMMListStart.Add(new SelectListItem { Text = "07", Value = "07" });
            TimeMMListStart.Add(new SelectListItem { Text = "08", Value = "08" });
            TimeMMListStart.Add(new SelectListItem { Text = "09", Value = "09" });
            TimeMMListStart.Add(new SelectListItem { Text = "10", Value = "10" });
            TimeMMListStart.Add(new SelectListItem { Text = "11", Value = "11" });
            TimeMMListStart.Add(new SelectListItem { Text = "12", Value = "12" });
            TimeMMListStart.Add(new SelectListItem { Text = "13", Value = "13" });
            TimeMMListStart.Add(new SelectListItem { Text = "14", Value = "14" });
            TimeMMListStart.Add(new SelectListItem { Text = "15", Value = "15" });
            TimeMMListStart.Add(new SelectListItem { Text = "16", Value = "16" });
            TimeMMListStart.Add(new SelectListItem { Text = "17", Value = "17" });
            TimeMMListStart.Add(new SelectListItem { Text = "18", Value = "18" });
            TimeMMListStart.Add(new SelectListItem { Text = "19", Value = "19" });
            TimeMMListStart.Add(new SelectListItem { Text = "20", Value = "20" });
            TimeMMListStart.Add(new SelectListItem { Text = "21", Value = "21" });
            TimeMMListStart.Add(new SelectListItem { Text = "22", Value = "22" });
            TimeMMListStart.Add(new SelectListItem { Text = "23", Value = "23" });
            TimeMMListStart.Add(new SelectListItem() { Text = "24", Value = "24" });
            TimeMMListStart.Add(new SelectListItem() { Text = "25", Value = "25" });
            TimeMMListStart.Add(new SelectListItem() { Text = "26", Value = "26" });
            TimeMMListStart.Add(new SelectListItem() { Text = "27", Value = "27" });
            TimeMMListStart.Add(new SelectListItem() { Text = "28", Value = "28" });
            TimeMMListStart.Add(new SelectListItem() { Text = "29", Value = "29" });
            TimeMMListStart.Add(new SelectListItem() { Text = "30", Value = "30" });
            TimeMMListStart.Add(new SelectListItem() { Text = "31", Value = "31" });
            TimeMMListStart.Add(new SelectListItem() { Text = "32", Value = "32" });
            TimeMMListStart.Add(new SelectListItem() { Text = "33", Value = "33" });
            TimeMMListStart.Add(new SelectListItem() { Text = "34", Value = "34" });
            TimeMMListStart.Add(new SelectListItem() { Text = "35", Value = "35" });
            TimeMMListStart.Add(new SelectListItem() { Text = "36", Value = "36" });
            TimeMMListStart.Add(new SelectListItem() { Text = "37", Value = "37" });
            TimeMMListStart.Add(new SelectListItem() { Text = "38", Value = "38" });
            TimeMMListStart.Add(new SelectListItem() { Text = "39", Value = "39" });
            TimeMMListStart.Add(new SelectListItem() { Text = "40", Value = "40" });
            TimeMMListStart.Add(new SelectListItem() { Text = "41", Value = "41" });
            TimeMMListStart.Add(new SelectListItem() { Text = "42", Value = "42" });
            TimeMMListStart.Add(new SelectListItem() { Text = "43", Value = "43" });
            TimeMMListStart.Add(new SelectListItem() { Text = "44", Value = "44" });
            TimeMMListStart.Add(new SelectListItem() { Text = "45", Value = "45" });
            TimeMMListStart.Add(new SelectListItem() { Text = "46", Value = "46" });
            TimeMMListStart.Add(new SelectListItem() { Text = "47", Value = "47" });
            TimeMMListStart.Add(new SelectListItem() { Text = "48", Value = "48" });
            TimeMMListStart.Add(new SelectListItem() { Text = "49", Value = "49" });
            TimeMMListStart.Add(new SelectListItem() { Text = "50", Value = "50" });
            TimeMMListStart.Add(new SelectListItem() { Text = "51", Value = "51" });
            TimeMMListStart.Add(new SelectListItem() { Text = "52", Value = "52" });
            TimeMMListStart.Add(new SelectListItem() { Text = "53", Value = "53" });
            TimeMMListStart.Add(new SelectListItem() { Text = "54", Value = "54" });
            TimeMMListStart.Add(new SelectListItem() { Text = "55", Value = "55" });
            TimeMMListStart.Add(new SelectListItem() { Text = "56", Value = "56" });
            TimeMMListStart.Add(new SelectListItem() { Text = "57", Value = "57" });
            TimeMMListStart.Add(new SelectListItem() { Text = "58", Value = "58" });
            TimeMMListStart.Add(new SelectListItem() { Text = "59", Value = "59" });

            ViewBag.UhrMMStart = TimeMMListStart;
            //Ende

            var TimeHHListEnde = new List<SelectListItem>();
            TimeHHListEnde.Add(new SelectListItem { Text = "00", Value = "00" });
            TimeHHListEnde.Add(new SelectListItem { Text = "01", Value = "01" });
            TimeHHListEnde.Add(new SelectListItem { Text = "02", Value = "02" });
            TimeHHListEnde.Add(new SelectListItem { Text = "03", Value = "03" });
            TimeHHListEnde.Add(new SelectListItem { Text = "04", Value = "04" });
            TimeHHListEnde.Add(new SelectListItem { Text = "05", Value = "05" });
            TimeHHListEnde.Add(new SelectListItem { Text = "06", Value = "06" });
            TimeHHListEnde.Add(new SelectListItem { Text = "07", Value = "07" });
            TimeHHListEnde.Add(new SelectListItem { Text = "08", Value = "08" });
            TimeHHListEnde.Add(new SelectListItem { Text = "09", Value = "09" });
            TimeHHListEnde.Add(new SelectListItem { Text = "10", Value = "10" });
            TimeHHListEnde.Add(new SelectListItem { Text = "11", Value = "11" });
            TimeHHListEnde.Add(new SelectListItem { Text = "12", Value = "12" });
            TimeHHListEnde.Add(new SelectListItem { Text = "13", Value = "13" });
            TimeHHListEnde.Add(new SelectListItem { Text = "14", Value = "14" });
            TimeHHListEnde.Add(new SelectListItem { Text = "15", Value = "15" });
            TimeHHListEnde.Add(new SelectListItem { Text = "16", Value = "16" });
            TimeHHListEnde.Add(new SelectListItem { Text = "17", Value = "17" });
            TimeHHListEnde.Add(new SelectListItem { Text = "18", Value = "18" });
            TimeHHListEnde.Add(new SelectListItem { Text = "19", Value = "19" });
            TimeHHListEnde.Add(new SelectListItem { Text = "20", Value = "20" });
            TimeHHListEnde.Add(new SelectListItem { Text = "21", Value = "21" });
            TimeHHListEnde.Add(new SelectListItem { Text = "22", Value = "22" });
            TimeHHListEnde.Add(new SelectListItem { Text = "23", Value = "23" });

            ViewBag.UhrHHEnde = TimeHHListEnde;

            var TimeMMListEnde = new List<SelectListItem>();
            TimeMMListEnde.Add(new SelectListItem { Text = "00", Value = "00" });
            TimeMMListEnde.Add(new SelectListItem { Text = "01", Value = "01" });
            TimeMMListEnde.Add(new SelectListItem { Text = "02", Value = "02" });
            TimeMMListEnde.Add(new SelectListItem { Text = "03", Value = "03" });
            TimeMMListEnde.Add(new SelectListItem { Text = "04", Value = "04" });
            TimeMMListEnde.Add(new SelectListItem { Text = "05", Value = "05" });
            TimeMMListEnde.Add(new SelectListItem { Text = "06", Value = "06" });
            TimeMMListEnde.Add(new SelectListItem { Text = "07", Value = "07" });
            TimeMMListEnde.Add(new SelectListItem { Text = "08", Value = "08" });
            TimeMMListEnde.Add(new SelectListItem { Text = "09", Value = "09" });
            TimeMMListEnde.Add(new SelectListItem { Text = "10", Value = "10" });
            TimeMMListEnde.Add(new SelectListItem { Text = "11", Value = "11" });
            TimeMMListEnde.Add(new SelectListItem { Text = "12", Value = "12" });
            TimeMMListEnde.Add(new SelectListItem { Text = "13", Value = "13" });
            TimeMMListEnde.Add(new SelectListItem { Text = "14", Value = "14" });
            TimeMMListEnde.Add(new SelectListItem { Text = "15", Value = "15" });
            TimeMMListEnde.Add(new SelectListItem { Text = "16", Value = "16" });
            TimeMMListEnde.Add(new SelectListItem { Text = "17", Value = "17" });
            TimeMMListEnde.Add(new SelectListItem { Text = "18", Value = "18" });
            TimeMMListEnde.Add(new SelectListItem { Text = "19", Value = "19" });
            TimeMMListEnde.Add(new SelectListItem { Text = "20", Value = "20" });
            TimeMMListEnde.Add(new SelectListItem { Text = "21", Value = "21" });
            TimeMMListEnde.Add(new SelectListItem { Text = "22", Value = "22" });
            TimeMMListEnde.Add(new SelectListItem { Text = "23", Value = "23" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "24", Value = "24" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "25", Value = "25" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "26", Value = "26" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "27", Value = "27" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "28", Value = "28" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "29", Value = "29" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "30", Value = "30" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "31", Value = "31" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "32", Value = "32" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "33", Value = "33" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "34", Value = "34" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "35", Value = "35" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "36", Value = "36" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "37", Value = "37" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "38", Value = "38" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "39", Value = "39" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "40", Value = "40" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "41", Value = "41" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "42", Value = "42" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "43", Value = "43" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "44", Value = "44" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "45", Value = "45" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "46", Value = "46" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "47", Value = "47" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "48", Value = "48" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "49", Value = "49" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "50", Value = "50" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "51", Value = "51" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "52", Value = "52" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "53", Value = "53" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "54", Value = "54" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "55", Value = "55" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "56", Value = "56" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "57", Value = "57" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "58", Value = "58" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "59", Value = "59" });

            ViewBag.UhrMMEnde = TimeMMListEnde;



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
                string stunde = Request.Form["UhrHHStart"].ToString();
                int std = System.Convert.ToInt32(stunde);
                string minute = Request.Form["UhrMMStart"].ToString();
                int min = System.Convert.ToInt32(minute);


                System.DateTime start = new System.DateTime(localCase.timecreate.Year, localCase.timecreate.Month,
                    localCase.timecreate.Day, std, min, 0);
                localCase.timecreate = start;

                string stunde2 = Request.Form["UhrHHEnde"].ToString();
                int std2 = System.Convert.ToInt32(stunde2);
                string minute2 = Request.Form["UhrMMEnde"].ToString();
                int min2 = System.Convert.ToInt32(minute2);


                System.DateTime ende = new System.DateTime(localCase.timeclosed.Year, localCase.timeclosed.Month,
                    localCase.timeclosed.Day, std2, min2, 0);

                localCase.timeclosed = ende;
                localCase.isactive = true;
                localCase.diagnosis = "offen";
                localCase.medication = "offen";
                localCase.therapy = "offen";
                localCase.expectedtime = "offen";
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
            if (GlobalVariable.currentRole.Equals("Arzt"))
            {
                myLayoutName = "_Layout_Arzt";
            }
            LocalCase loc = new LocalCase();
            loc.timecreate = DateTime.Now;
            loc.timemodify = DateTime.Now;
            ViewResult myView = View(loc);
            myView.MasterName = myLayoutName;

            //Für Uhrzeit:
            var TimeHHListStart = new List<SelectListItem>();
            TimeHHListStart.Add (new SelectListItem { Text = "00", Value = "00" });
            TimeHHListStart.Add(new SelectListItem { Text = "01", Value = "01" });
            TimeHHListStart.Add(new SelectListItem { Text = "02", Value = "02" });
            TimeHHListStart.Add(new SelectListItem { Text = "03", Value = "03" });
            TimeHHListStart.Add( new SelectListItem { Text = "04", Value = "04" });
            TimeHHListStart.Add(new SelectListItem { Text = "05", Value = "05" });
            TimeHHListStart.Add(new SelectListItem { Text = "06", Value = "06" });
            TimeHHListStart.Add(new SelectListItem { Text = "07", Value = "07" });
            TimeHHListStart.Add(new SelectListItem { Text = "08", Value = "08" });
            TimeHHListStart.Add(new SelectListItem { Text = "09", Value = "09" });
            TimeHHListStart.Add(new SelectListItem { Text = "10", Value = "10" });
            TimeHHListStart.Add(new SelectListItem { Text = "11", Value = "11" });
            TimeHHListStart.Add(new SelectListItem { Text = "12", Value = "12" });
            TimeHHListStart.Add(new SelectListItem { Text = "13", Value = "13" });
            TimeHHListStart.Add(new SelectListItem { Text = "14", Value = "14" });
            TimeHHListStart.Add(new SelectListItem { Text = "15", Value = "15" });
            TimeHHListStart.Add(new SelectListItem { Text = "16", Value = "16" });
            TimeHHListStart.Add(new SelectListItem { Text = "17", Value = "17" });
            TimeHHListStart.Add(new SelectListItem { Text = "18", Value = "18" });
            TimeHHListStart.Add(new SelectListItem { Text = "19", Value = "19" });
            TimeHHListStart.Add(new SelectListItem { Text = "20", Value = "20" });
            TimeHHListStart.Add(new SelectListItem { Text = "21", Value = "21" });
            TimeHHListStart.Add(new SelectListItem { Text = "22", Value = "22" });
            TimeHHListStart.Add(new SelectListItem { Text = "23", Value = "23" });

            ViewBag.UhrHHStart = TimeHHListStart;

            var TimeMMListStart = new List<SelectListItem>();
            TimeMMListStart.Add(new SelectListItem { Text = "00", Value = "00" });
            TimeMMListStart.Add(new SelectListItem { Text = "01", Value = "01" });
            TimeMMListStart.Add(new SelectListItem { Text = "02", Value = "02" });
            TimeMMListStart.Add(new SelectListItem { Text = "03", Value = "03" });
            TimeMMListStart.Add(new SelectListItem { Text = "04", Value = "04" });
            TimeMMListStart.Add(new SelectListItem { Text = "05", Value = "05" });
            TimeMMListStart.Add(new SelectListItem { Text = "06", Value = "06" });
            TimeMMListStart.Add(new SelectListItem { Text = "07", Value = "07" });
            TimeMMListStart.Add(new SelectListItem { Text = "08", Value = "08" });
            TimeMMListStart.Add(new SelectListItem { Text = "09", Value = "09" });
            TimeMMListStart.Add(new SelectListItem { Text = "10", Value = "10" });
            TimeMMListStart.Add(new SelectListItem { Text = "11", Value = "11" });
            TimeMMListStart.Add(new SelectListItem { Text = "12", Value = "12" });
            TimeMMListStart.Add(new SelectListItem { Text = "13", Value = "13" });
            TimeMMListStart.Add(new SelectListItem { Text = "14", Value = "14" });
            TimeMMListStart.Add(new SelectListItem { Text = "15", Value = "15" });
            TimeMMListStart.Add(new SelectListItem { Text = "16", Value = "16" });
            TimeMMListStart.Add(new SelectListItem { Text = "17", Value = "17" });
            TimeMMListStart.Add(new SelectListItem { Text = "18", Value = "18" });
            TimeMMListStart.Add(new SelectListItem { Text = "19", Value = "19" });
            TimeMMListStart.Add(new SelectListItem { Text = "20", Value = "20" });
            TimeMMListStart.Add(new SelectListItem { Text = "21", Value = "21" });
            TimeMMListStart.Add(new SelectListItem { Text = "22", Value = "22" });
            TimeMMListStart.Add(new SelectListItem { Text = "23", Value = "23" });
            TimeMMListStart.Add(new SelectListItem() { Text = "24", Value = "24" });
            TimeMMListStart.Add(new SelectListItem() { Text = "25", Value = "25" });
            TimeMMListStart.Add(new SelectListItem() { Text = "26", Value = "26" });
            TimeMMListStart.Add(new SelectListItem() { Text = "27", Value = "27" });
            TimeMMListStart.Add(new SelectListItem() { Text = "28", Value = "28" });
            TimeMMListStart.Add(new SelectListItem() { Text = "29", Value = "29" });
            TimeMMListStart.Add(new SelectListItem() { Text = "30", Value = "30" });
            TimeMMListStart.Add(new SelectListItem() { Text = "31", Value = "31" });
            TimeMMListStart.Add(new SelectListItem() { Text = "32", Value = "32" });
            TimeMMListStart.Add(new SelectListItem() { Text = "33", Value = "33" });
            TimeMMListStart.Add(new SelectListItem() { Text = "34", Value = "34" });
            TimeMMListStart.Add(new SelectListItem() { Text = "35", Value = "35" });
            TimeMMListStart.Add(new SelectListItem() { Text = "36", Value = "36" });
            TimeMMListStart.Add(new SelectListItem() { Text = "37", Value = "37" });
            TimeMMListStart.Add(new SelectListItem() { Text = "38", Value = "38" });
            TimeMMListStart.Add(new SelectListItem() { Text = "39", Value = "39" });
            TimeMMListStart.Add(new SelectListItem() { Text = "40", Value = "40" });
             TimeMMListStart.Add(new SelectListItem() { Text = "41", Value = "41" });
             TimeMMListStart.Add(new SelectListItem() { Text = "42", Value = "42" });
             TimeMMListStart.Add(new SelectListItem() { Text = "43", Value = "43" });
             TimeMMListStart.Add(new SelectListItem() { Text = "44", Value = "44" });
             TimeMMListStart.Add(new SelectListItem() { Text = "45", Value = "45" });
             TimeMMListStart.Add(new SelectListItem() { Text = "46", Value = "46" });
             TimeMMListStart.Add(new SelectListItem() { Text = "47", Value = "47" });
             TimeMMListStart.Add(new SelectListItem() { Text = "48", Value = "48" });
             TimeMMListStart.Add(new SelectListItem() { Text = "49", Value = "49" });
                    TimeMMListStart.Add(new SelectListItem() { Text = "50", Value = "50" });
                   TimeMMListStart.Add(new SelectListItem() { Text = "51", Value = "51" });
                   TimeMMListStart.Add(new SelectListItem() { Text = "52", Value = "52" });
                   TimeMMListStart.Add(new SelectListItem() { Text = "53", Value = "53" });
                   TimeMMListStart.Add(new SelectListItem() { Text = "54", Value = "54" });
                   TimeMMListStart.Add(new SelectListItem() { Text = "55", Value = "55" });
                   TimeMMListStart.Add(new SelectListItem() { Text = "56", Value = "56" });
                  TimeMMListStart.Add(new SelectListItem() { Text = "57", Value = "57" });
                  TimeMMListStart.Add(new SelectListItem() { Text = "58", Value = "58" });
                  TimeMMListStart.Add(new SelectListItem() { Text = "59", Value = "59" });

            ViewBag.UhrMMStart = TimeMMListStart;
            //Ende

            var TimeHHListEnde = new List<SelectListItem>();
            TimeHHListEnde.Add(new SelectListItem { Text = "00", Value = "00" });
            TimeHHListEnde.Add(new SelectListItem { Text = "01", Value = "01" });
            TimeHHListEnde.Add(new SelectListItem { Text = "02", Value = "02" });
            TimeHHListEnde.Add(new SelectListItem { Text = "03", Value = "03" });
            TimeHHListEnde.Add(new SelectListItem { Text = "04", Value = "04" });
            TimeHHListEnde.Add(new SelectListItem { Text = "05", Value = "05" });
            TimeHHListEnde.Add(new SelectListItem { Text = "06", Value = "06" });
            TimeHHListEnde.Add(new SelectListItem { Text = "07", Value = "07" });
            TimeHHListEnde.Add(new SelectListItem { Text = "08", Value = "08" });
            TimeHHListEnde.Add(new SelectListItem { Text = "09", Value = "09" });
            TimeHHListEnde.Add(new SelectListItem { Text = "10", Value = "10" });
            TimeHHListEnde.Add(new SelectListItem { Text = "11", Value = "11" });
            TimeHHListEnde.Add(new SelectListItem { Text = "12", Value = "12" });
            TimeHHListEnde.Add(new SelectListItem { Text = "13", Value = "13" });
            TimeHHListEnde.Add(new SelectListItem { Text = "14", Value = "14" });
            TimeHHListEnde.Add(new SelectListItem { Text = "15", Value = "15" });
            TimeHHListEnde.Add(new SelectListItem { Text = "16", Value = "16" });
            TimeHHListEnde.Add(new SelectListItem { Text = "17", Value = "17" });
            TimeHHListEnde.Add(new SelectListItem { Text = "18", Value = "18" });
            TimeHHListEnde.Add(new SelectListItem { Text = "19", Value = "19" });
            TimeHHListEnde.Add(new SelectListItem { Text = "20", Value = "20" });
            TimeHHListEnde.Add(new SelectListItem { Text = "21", Value = "21" });
            TimeHHListEnde.Add(new SelectListItem { Text = "22", Value = "22" });
            TimeHHListEnde.Add(new SelectListItem { Text = "23", Value = "23" });

            ViewBag.UhrHHEnde = TimeHHListEnde;

            var TimeMMListEnde = new List<SelectListItem>();
            TimeMMListEnde.Add(new SelectListItem { Text = "00", Value = "00" });
            TimeMMListEnde.Add(new SelectListItem { Text = "01", Value = "01" });
            TimeMMListEnde.Add(new SelectListItem { Text = "02", Value = "02" });
            TimeMMListEnde.Add(new SelectListItem { Text = "03", Value = "03" });
            TimeMMListEnde.Add(new SelectListItem { Text = "04", Value = "04" });
            TimeMMListEnde.Add(new SelectListItem { Text = "05", Value = "05" });
            TimeMMListEnde.Add(new SelectListItem { Text = "06", Value = "06" });
            TimeMMListEnde.Add(new SelectListItem { Text = "07", Value = "07" });
            TimeMMListEnde.Add(new SelectListItem { Text = "08", Value = "08" });
            TimeMMListEnde.Add(new SelectListItem { Text = "09", Value = "09" });
            TimeMMListEnde.Add(new SelectListItem { Text = "10", Value = "10" });
            TimeMMListEnde.Add(new SelectListItem { Text = "11", Value = "11" });
            TimeMMListEnde.Add(new SelectListItem { Text = "12", Value = "12" });
            TimeMMListEnde.Add(new SelectListItem { Text = "13", Value = "13" });
            TimeMMListEnde.Add(new SelectListItem { Text = "14", Value = "14" });
            TimeMMListEnde.Add(new SelectListItem { Text = "15", Value = "15" });
            TimeMMListEnde.Add(new SelectListItem { Text = "16", Value = "16" });
            TimeMMListEnde.Add(new SelectListItem { Text = "17", Value = "17" });
            TimeMMListEnde.Add(new SelectListItem { Text = "18", Value = "18" });
            TimeMMListEnde.Add(new SelectListItem { Text = "19", Value = "19" });
            TimeMMListEnde.Add(new SelectListItem { Text = "20", Value = "20" });
            TimeMMListEnde.Add(new SelectListItem { Text = "21", Value = "21" });
            TimeMMListEnde.Add(new SelectListItem { Text = "22", Value = "22" });
            TimeMMListEnde.Add(new SelectListItem { Text = "23", Value = "23" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "24", Value = "24" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "25", Value = "25" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "26", Value = "26" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "27", Value = "27" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "28", Value = "28" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "29", Value = "29" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "30", Value = "30" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "31", Value = "31" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "32", Value = "32" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "33", Value = "33" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "34", Value = "34" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "35", Value = "35" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "36", Value = "36" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "37", Value = "37" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "38", Value = "38" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "39", Value = "39" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "40", Value = "40" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "41", Value = "41" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "42", Value = "42" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "43", Value = "43" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "44", Value = "44" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "45", Value = "45" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "46", Value = "46" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "47", Value = "47" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "48", Value = "48" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "49", Value = "49" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "50", Value = "50" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "51", Value = "51" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "52", Value = "52" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "53", Value = "53" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "54", Value = "54" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "55", Value = "55" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "56", Value = "56" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "57", Value = "57" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "58", Value = "58" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "59", Value = "59" });

            ViewBag.UhrMMEnde = TimeMMListEnde;



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
                string stunde = Request.Form["UhrHHStart"].ToString();
                int std = System.Convert.ToInt32(stunde);
                string minute = Request.Form["UhrMMStart"].ToString();
                int min = System.Convert.ToInt32(minute);


                System.DateTime start = new System.DateTime(localCase.timecreate.Year, localCase.timecreate.Month, 
                    localCase.timecreate.Day, std, min, 0);
                localCase.timecreate = start;

                string stunde2 = Request.Form["UhrHHEnde"].ToString();
                int std2 = System.Convert.ToInt32(stunde2);
                string minute2 = Request.Form["UhrMMEnde"].ToString();
                int min2 = System.Convert.ToInt32(minute2);


                System.DateTime ende = new System.DateTime(localCase.timeclosed.Year, localCase.timeclosed.Month,
                    localCase.timeclosed.Day, std2, min2, 0);

                localCase.timeclosed = ende;
                localCase.isactive = true;
                localCase.diagnosis = "offen";
                localCase.medication = "offen";
                localCase.therapy = "offen";
                localCase.expectedtime = "offen";
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
                return RedirectToAction("IndexArzt");
            }
            return View(localCase);
        }

        // GET: LocalCase/PatientAufnahme
        public ActionResult PatientAufnahmePflege()
        {
            String myLayoutName = "";
            if (GlobalVariable.currentRole.Equals("Pflegepersonal"))
            {
                myLayoutName = "_Layout_Schwester";
            }

            LocalCase loc = new LocalCase();
            loc.timecreate = DateTime.Now;
            loc.timemodify = DateTime.Now;
            ViewResult myView = View(loc);
            myView.MasterName = myLayoutName;

            //Für Uhrzeit:
            var TimeHHListStart = new List<SelectListItem>();
            TimeHHListStart.Add(new SelectListItem { Text = "00", Value = "00" });
            TimeHHListStart.Add(new SelectListItem { Text = "01", Value = "01" });
            TimeHHListStart.Add(new SelectListItem { Text = "02", Value = "02" });
            TimeHHListStart.Add(new SelectListItem { Text = "03", Value = "03" });
            TimeHHListStart.Add(new SelectListItem { Text = "04", Value = "04" });
            TimeHHListStart.Add(new SelectListItem { Text = "05", Value = "05" });
            TimeHHListStart.Add(new SelectListItem { Text = "06", Value = "06" });
            TimeHHListStart.Add(new SelectListItem { Text = "07", Value = "07" });
            TimeHHListStart.Add(new SelectListItem { Text = "08", Value = "08" });
            TimeHHListStart.Add(new SelectListItem { Text = "09", Value = "09" });
            TimeHHListStart.Add(new SelectListItem { Text = "10", Value = "10" });
            TimeHHListStart.Add(new SelectListItem { Text = "11", Value = "11" });
            TimeHHListStart.Add(new SelectListItem { Text = "12", Value = "12" });
            TimeHHListStart.Add(new SelectListItem { Text = "13", Value = "13" });
            TimeHHListStart.Add(new SelectListItem { Text = "14", Value = "14" });
            TimeHHListStart.Add(new SelectListItem { Text = "15", Value = "15" });
            TimeHHListStart.Add(new SelectListItem { Text = "16", Value = "16" });
            TimeHHListStart.Add(new SelectListItem { Text = "17", Value = "17" });
            TimeHHListStart.Add(new SelectListItem { Text = "18", Value = "18" });
            TimeHHListStart.Add(new SelectListItem { Text = "19", Value = "19" });
            TimeHHListStart.Add(new SelectListItem { Text = "20", Value = "20" });
            TimeHHListStart.Add(new SelectListItem { Text = "21", Value = "21" });
            TimeHHListStart.Add(new SelectListItem { Text = "22", Value = "22" });
            TimeHHListStart.Add(new SelectListItem { Text = "23", Value = "23" });

            ViewBag.UhrHHStart = TimeHHListStart;

            var TimeMMListStart = new List<SelectListItem>();
            TimeMMListStart.Add(new SelectListItem { Text = "00", Value = "00" });
            TimeMMListStart.Add(new SelectListItem { Text = "01", Value = "01" });
            TimeMMListStart.Add(new SelectListItem { Text = "02", Value = "02" });
            TimeMMListStart.Add(new SelectListItem { Text = "03", Value = "03" });
            TimeMMListStart.Add(new SelectListItem { Text = "04", Value = "04" });
            TimeMMListStart.Add(new SelectListItem { Text = "05", Value = "05" });
            TimeMMListStart.Add(new SelectListItem { Text = "06", Value = "06" });
            TimeMMListStart.Add(new SelectListItem { Text = "07", Value = "07" });
            TimeMMListStart.Add(new SelectListItem { Text = "08", Value = "08" });
            TimeMMListStart.Add(new SelectListItem { Text = "09", Value = "09" });
            TimeMMListStart.Add(new SelectListItem { Text = "10", Value = "10" });
            TimeMMListStart.Add(new SelectListItem { Text = "11", Value = "11" });
            TimeMMListStart.Add(new SelectListItem { Text = "12", Value = "12" });
            TimeMMListStart.Add(new SelectListItem { Text = "13", Value = "13" });
            TimeMMListStart.Add(new SelectListItem { Text = "14", Value = "14" });
            TimeMMListStart.Add(new SelectListItem { Text = "15", Value = "15" });
            TimeMMListStart.Add(new SelectListItem { Text = "16", Value = "16" });
            TimeMMListStart.Add(new SelectListItem { Text = "17", Value = "17" });
            TimeMMListStart.Add(new SelectListItem { Text = "18", Value = "18" });
            TimeMMListStart.Add(new SelectListItem { Text = "19", Value = "19" });
            TimeMMListStart.Add(new SelectListItem { Text = "20", Value = "20" });
            TimeMMListStart.Add(new SelectListItem { Text = "21", Value = "21" });
            TimeMMListStart.Add(new SelectListItem { Text = "22", Value = "22" });
            TimeMMListStart.Add(new SelectListItem { Text = "23", Value = "23" });
            TimeMMListStart.Add(new SelectListItem() { Text = "24", Value = "24" });
            TimeMMListStart.Add(new SelectListItem() { Text = "25", Value = "25" });
            TimeMMListStart.Add(new SelectListItem() { Text = "26", Value = "26" });
            TimeMMListStart.Add(new SelectListItem() { Text = "27", Value = "27" });
            TimeMMListStart.Add(new SelectListItem() { Text = "28", Value = "28" });
            TimeMMListStart.Add(new SelectListItem() { Text = "29", Value = "29" });
            TimeMMListStart.Add(new SelectListItem() { Text = "30", Value = "30" });
            TimeMMListStart.Add(new SelectListItem() { Text = "31", Value = "31" });
            TimeMMListStart.Add(new SelectListItem() { Text = "32", Value = "32" });
            TimeMMListStart.Add(new SelectListItem() { Text = "33", Value = "33" });
            TimeMMListStart.Add(new SelectListItem() { Text = "34", Value = "34" });
            TimeMMListStart.Add(new SelectListItem() { Text = "35", Value = "35" });
            TimeMMListStart.Add(new SelectListItem() { Text = "36", Value = "36" });
            TimeMMListStart.Add(new SelectListItem() { Text = "37", Value = "37" });
            TimeMMListStart.Add(new SelectListItem() { Text = "38", Value = "38" });
            TimeMMListStart.Add(new SelectListItem() { Text = "39", Value = "39" });
            TimeMMListStart.Add(new SelectListItem() { Text = "40", Value = "40" });
            TimeMMListStart.Add(new SelectListItem() { Text = "41", Value = "41" });
            TimeMMListStart.Add(new SelectListItem() { Text = "42", Value = "42" });
            TimeMMListStart.Add(new SelectListItem() { Text = "43", Value = "43" });
            TimeMMListStart.Add(new SelectListItem() { Text = "44", Value = "44" });
            TimeMMListStart.Add(new SelectListItem() { Text = "45", Value = "45" });
            TimeMMListStart.Add(new SelectListItem() { Text = "46", Value = "46" });
            TimeMMListStart.Add(new SelectListItem() { Text = "47", Value = "47" });
            TimeMMListStart.Add(new SelectListItem() { Text = "48", Value = "48" });
            TimeMMListStart.Add(new SelectListItem() { Text = "49", Value = "49" });
            TimeMMListStart.Add(new SelectListItem() { Text = "50", Value = "50" });
            TimeMMListStart.Add(new SelectListItem() { Text = "51", Value = "51" });
            TimeMMListStart.Add(new SelectListItem() { Text = "52", Value = "52" });
            TimeMMListStart.Add(new SelectListItem() { Text = "53", Value = "53" });
            TimeMMListStart.Add(new SelectListItem() { Text = "54", Value = "54" });
            TimeMMListStart.Add(new SelectListItem() { Text = "55", Value = "55" });
            TimeMMListStart.Add(new SelectListItem() { Text = "56", Value = "56" });
            TimeMMListStart.Add(new SelectListItem() { Text = "57", Value = "57" });
            TimeMMListStart.Add(new SelectListItem() { Text = "58", Value = "58" });
            TimeMMListStart.Add(new SelectListItem() { Text = "59", Value = "59" });

            ViewBag.UhrMMStart = TimeMMListStart;
            //Ende

            var TimeHHListEnde = new List<SelectListItem>();
            TimeHHListEnde.Add(new SelectListItem { Text = "00", Value = "00" });
            TimeHHListEnde.Add(new SelectListItem { Text = "01", Value = "01" });
            TimeHHListEnde.Add(new SelectListItem { Text = "02", Value = "02" });
            TimeHHListEnde.Add(new SelectListItem { Text = "03", Value = "03" });
            TimeHHListEnde.Add(new SelectListItem { Text = "04", Value = "04" });
            TimeHHListEnde.Add(new SelectListItem { Text = "05", Value = "05" });
            TimeHHListEnde.Add(new SelectListItem { Text = "06", Value = "06" });
            TimeHHListEnde.Add(new SelectListItem { Text = "07", Value = "07" });
            TimeHHListEnde.Add(new SelectListItem { Text = "08", Value = "08" });
            TimeHHListEnde.Add(new SelectListItem { Text = "09", Value = "09" });
            TimeHHListEnde.Add(new SelectListItem { Text = "10", Value = "10" });
            TimeHHListEnde.Add(new SelectListItem { Text = "11", Value = "11" });
            TimeHHListEnde.Add(new SelectListItem { Text = "12", Value = "12" });
            TimeHHListEnde.Add(new SelectListItem { Text = "13", Value = "13" });
            TimeHHListEnde.Add(new SelectListItem { Text = "14", Value = "14" });
            TimeHHListEnde.Add(new SelectListItem { Text = "15", Value = "15" });
            TimeHHListEnde.Add(new SelectListItem { Text = "16", Value = "16" });
            TimeHHListEnde.Add(new SelectListItem { Text = "17", Value = "17" });
            TimeHHListEnde.Add(new SelectListItem { Text = "18", Value = "18" });
            TimeHHListEnde.Add(new SelectListItem { Text = "19", Value = "19" });
            TimeHHListEnde.Add(new SelectListItem { Text = "20", Value = "20" });
            TimeHHListEnde.Add(new SelectListItem { Text = "21", Value = "21" });
            TimeHHListEnde.Add(new SelectListItem { Text = "22", Value = "22" });
            TimeHHListEnde.Add(new SelectListItem { Text = "23", Value = "23" });

            ViewBag.UhrHHEnde = TimeHHListEnde;

            var TimeMMListEnde = new List<SelectListItem>();
            TimeMMListEnde.Add(new SelectListItem { Text = "00", Value = "00" });
            TimeMMListEnde.Add(new SelectListItem { Text = "01", Value = "01" });
            TimeMMListEnde.Add(new SelectListItem { Text = "02", Value = "02" });
            TimeMMListEnde.Add(new SelectListItem { Text = "03", Value = "03" });
            TimeMMListEnde.Add(new SelectListItem { Text = "04", Value = "04" });
            TimeMMListEnde.Add(new SelectListItem { Text = "05", Value = "05" });
            TimeMMListEnde.Add(new SelectListItem { Text = "06", Value = "06" });
            TimeMMListEnde.Add(new SelectListItem { Text = "07", Value = "07" });
            TimeMMListEnde.Add(new SelectListItem { Text = "08", Value = "08" });
            TimeMMListEnde.Add(new SelectListItem { Text = "09", Value = "09" });
            TimeMMListEnde.Add(new SelectListItem { Text = "10", Value = "10" });
            TimeMMListEnde.Add(new SelectListItem { Text = "11", Value = "11" });
            TimeMMListEnde.Add(new SelectListItem { Text = "12", Value = "12" });
            TimeMMListEnde.Add(new SelectListItem { Text = "13", Value = "13" });
            TimeMMListEnde.Add(new SelectListItem { Text = "14", Value = "14" });
            TimeMMListEnde.Add(new SelectListItem { Text = "15", Value = "15" });
            TimeMMListEnde.Add(new SelectListItem { Text = "16", Value = "16" });
            TimeMMListEnde.Add(new SelectListItem { Text = "17", Value = "17" });
            TimeMMListEnde.Add(new SelectListItem { Text = "18", Value = "18" });
            TimeMMListEnde.Add(new SelectListItem { Text = "19", Value = "19" });
            TimeMMListEnde.Add(new SelectListItem { Text = "20", Value = "20" });
            TimeMMListEnde.Add(new SelectListItem { Text = "21", Value = "21" });
            TimeMMListEnde.Add(new SelectListItem { Text = "22", Value = "22" });
            TimeMMListEnde.Add(new SelectListItem { Text = "23", Value = "23" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "24", Value = "24" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "25", Value = "25" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "26", Value = "26" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "27", Value = "27" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "28", Value = "28" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "29", Value = "29" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "30", Value = "30" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "31", Value = "31" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "32", Value = "32" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "33", Value = "33" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "34", Value = "34" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "35", Value = "35" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "36", Value = "36" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "37", Value = "37" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "38", Value = "38" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "39", Value = "39" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "40", Value = "40" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "41", Value = "41" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "42", Value = "42" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "43", Value = "43" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "44", Value = "44" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "45", Value = "45" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "46", Value = "46" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "47", Value = "47" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "48", Value = "48" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "49", Value = "49" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "50", Value = "50" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "51", Value = "51" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "52", Value = "52" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "53", Value = "53" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "54", Value = "54" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "55", Value = "55" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "56", Value = "56" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "57", Value = "57" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "58", Value = "58" });
            TimeMMListEnde.Add(new SelectListItem() { Text = "59", Value = "59" });

            ViewBag.UhrMMEnde = TimeMMListEnde;

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
                string stunde = Request.Form["UhrHHStart"].ToString();
                int std = System.Convert.ToInt32(stunde);
                string minute = Request.Form["UhrMMStart"].ToString();
                int min = System.Convert.ToInt32(minute);


                System.DateTime start = new System.DateTime(localCase.timecreate.Year, localCase.timecreate.Month,
                    localCase.timecreate.Day, std, min, 0);
                localCase.timecreate = start;

                string stunde2 = Request.Form["UhrHHEnde"].ToString();
                int std2 = System.Convert.ToInt32(stunde2);
                string minute2 = Request.Form["UhrMMEnde"].ToString();
                int min2 = System.Convert.ToInt32(minute2);


                System.DateTime ende = new System.DateTime(localCase.timeclosed.Year, localCase.timeclosed.Month,
                    localCase.timeclosed.Day, std2, min2, 0);

                localCase.timeclosed = ende;
                localCase.isactive = true;
                localCase.diagnosis = "offen";
                localCase.medication = "offen";
                localCase.therapy = "offen";
                localCase.expectedtime = "offen";
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
                return RedirectToAction("IndexPfleger");
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
            

            for (int? i = 1; i < 80; i++)
            {
                try
                {
                    LocalCase loc = db.LocalCases.Find(i);
                    if (loc.isactive == true)
                    {
                        foreach (User user in loc.User)
                        {
                            if (user.rolename == "Arzt")
                            {
                                termine.Add(new LocalCase()
                                {
                                    casenr = loc.casenr,
                                    timecreate = loc.timecreate,
                                    timeclosed = loc.timeclosed,
                                    Id = loc.Id
                                });
                            }
                        }
                    }
                }
                catch (NullReferenceException)
                {

                }
                catch (InvalidOperationException e)
                {

                }

            }



            return Json(termine, JsonRequestBehavior.AllowGet);
        }
        //  --------------CK-End----------------------

        public JsonResult GetSchichtenPfleger()
        {
            List<LocalCase> termine = new List<LocalCase>();
            for (int? i = 1; i < 80; i++)
            {
                try
                {
                    LocalCase loc = db.LocalCases.Find(i);
                    if (loc.isactive == true)
                    {
                        foreach (User user in loc.User)
                        {
                            if (user.rolename == "Pflegepersonal")
                            {
                                termine.Add(new LocalCase()
                                {
                                    casenr = loc.casenr,
                                    timecreate = loc.timecreate,
                                    timeclosed = loc.timeclosed,
                                    Id = loc.Id
                                });
                            }
                        }
                    }
                }
                catch (NullReferenceException)
                {

                }
                catch (InvalidOperationException e)
                {

                }

            }



            return Json(termine, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSchichtenTherapeut()
        {
            List<LocalCase> termine = new List<LocalCase>();
            for (int? i = 1; i < 80; i++)
            {
                try
                {
                    LocalCase loc = db.LocalCases.Find(i);
                    if (loc.isactive == true)
                    {
                        foreach (User user in loc.User)
                        {
                            if (user.rolename == "Therapeut")
                            {
                                termine.Add(new LocalCase()
                                {
                                    casenr = loc.casenr,
                                    timecreate = loc.timecreate,
                                    timeclosed = loc.timeclosed,
                                    Id = loc.Id
                                });
                            }
                        }
                        
                    }
                }
                catch (NullReferenceException)
                {

                }
                catch (InvalidOperationException e)
                {

                }

            }



            return Json(termine, JsonRequestBehavior.AllowGet);
        }


    }
}
