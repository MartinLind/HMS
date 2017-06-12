using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test1()
        {
           
                ViewBag.Message = "This is a TestPage, for Staff!";

            return View();
        }
        public ActionResult LoginPage()
        {
      
            return View();
        }

        public ActionResult Test2()
        {
            ViewBag.Message = "Another TestPage, for Rooms!";
            return View();
        }

        public ActionResult Test3()
        {
            ViewBag.Message = "Another TestPage, for Patients!";
            return View();
        }
    }
}