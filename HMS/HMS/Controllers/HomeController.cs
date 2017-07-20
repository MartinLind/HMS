using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using HMS.Models;
using System.Net;

namespace HMS.Controllers
{
    public class HomeController : Controller
    {
        private DBContainer db = new DBContainer();
        // Author: Ming

        public ActionResult Index()
        {
            GlobalVariable.currentRole = GlobalVariable.Role.Admin;
            return View();
        }
        //public ActionResult LoginPage()
        //{
        
        //    return View();
        //}

        [HttpPost]
        public ActionResult Index(String username, String password)
        {
           
            if (db.Users.Where(x => x.rolename.Equals("Admin")).Count() < 1) {
                if (username == "Admin" && password == "admin")
                {
                    return View("Home", "_Layout_Admin");
                }
            }
            else
            {

              
            }
            //if (FunctionLogin(username, password) == true)
            //{
            //    /// if success
            //    return View("Home", "_Layout1");
            //}
            //else
            //{
            //    /// fail
            //    ViewBag.LoginMessage = "Something wrong! Try Again!";
            //    return View();
            //}

            ActionResult result = View();

            switch (FunctionLogin(username, password))
            {
                case FunctionLoginStatus.SUCCESS_Admin:
                    result = View("Home", "_Layout_Admin");
                    ViewBag.Logged = "Willkommen " + username;
                    GlobalVariable.currentRole = GlobalVariable.Role.Admin;
                    break;
                case FunctionLoginStatus.SUCCESS_Arzt:
                    result = View("Home", "_Layout_Arzt");
                    ViewBag.logged = "Willkommen " + username;
                    GlobalVariable.currentRole = GlobalVariable.Role.Arzt;
                    break;
                case FunctionLoginStatus.SUCCESS_Schwester:
                    result = View("Home", "_Layout_Schwester");
                    ViewBag.Logged = "Willkommen " + username;
                    GlobalVariable.currentRole = GlobalVariable.Role.Schwester;
                    break;
                case FunctionLoginStatus.SUCCESS_Reinigungspersonal:
                    result = View("Home", "_Layout_Reinigungspersonal");
                    ViewBag.Logged = "Willkommen " + username;
                    GlobalVariable.currentRole = GlobalVariable.Role.Reinigungspersonal;
                    break;
                case FunctionLoginStatus.SUCCESS_Therapeut:
                    result = View("Home", "_Layout_Therapeut");
                    ViewBag.Logged = "Willkommen " + username;
                    GlobalVariable.currentRole = GlobalVariable.Role.Therapeut;
                    break;
                case FunctionLoginStatus.FAIL:
                    ViewBag.LoginMessage = "Etwas ist schief gelaufen, versuchen Sie es erneut!";
                    break;
                case FunctionLoginStatus.FIRED:
                    ViewBag.LoginMessage = "Sie haben keinen Zugang mehr, bitte kontaktieren Sie ihren Vorgesetzten!";
                    break;
                //case FunctionLoginStatus.RETIRE:
                //    ViewBag.LoginMessage = "You are retired! goodbye!";
                //    break;
                //default:
                //    GlobalVariable.currentRole = GlobalVariable.Role.Unknown;
                //    break;
            }

            return result;
        }

        public enum FunctionLoginStatus
        {
            SUCCESS_Admin,
            SUCCESS_Arzt,
            SUCCESS_Schwester,
            SUCCESS_Reinigungspersonal,
            SUCCESS_Therapeut,
            FAIL,
            FIRED,
            RETIRE
        }

        public FunctionLoginStatus FunctionLogin(String username, String password)
        {
            SqlConnection connection = null;

            String ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=HMSDB1;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            connection = new SqlConnection(ConnectionString);

            String SQLString = String.Format("SELECT dbo.ObjectSet_User.username,dbo.ObjectSet_User.rolename,dbo.ObjectSet_User.accessright1,dbo.ObjectSet_User.accessright2,dbo.ObjectSet_User.accessright3,dbo.ObjectSet_User.accessright4,dbo.ObjectSet_User.accessright5,dbo.ObjectSet.isactive FROM dbo.ObjectSet_User LEFT JOIN dbo.ObjectSet ON dbo.ObjectSet_User.Id = dbo.ObjectSet.Id WHERE  username = '{0}' AND password = '{1}' ", username, password);

            SqlCommand cmd = new SqlCommand(SQLString, connection);

            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            String dbUsername = null;
            Boolean dbright_Admin = false;
            Boolean dbright_Arzt = false;
            Boolean dbright_Schwester = false;
            Boolean dbright_Reinigungspersonal = false;
            Boolean dbright_Therapeut = false;
            Boolean dbisactive = false;

            while (reader.Read() == true)
            {
                dbUsername = Convert.ToString(reader["username"]);
                /// ......
                dbright_Admin = Convert.ToBoolean(reader["accessright1"]);
                dbright_Arzt = Convert.ToBoolean(reader["accessright2"]);
                dbright_Schwester = Convert.ToBoolean(reader["accessright3"]);
                dbright_Reinigungspersonal = Convert.ToBoolean(reader["accessright4"]);
                dbright_Therapeut = Convert.ToBoolean(reader["accessright5"]);
                dbisactive = Convert.ToBoolean(reader["isactive"]);
               
                //in views its now renamed to status and set at the end of the list
                //we need to get this accessright5 for a new Layout_Therapeut, so we need to get the 
                //isactive boolean from object
               
            }

            if (dbUsername == null)
            {
                /// fail
                return FunctionLoginStatus.FAIL;
            }
            else
            {
                /// success
                if (dbright_Admin == true && dbisactive == true ) 
                {
                    /// success , and status==true
                    return FunctionLoginStatus.SUCCESS_Admin;
                }
                if (dbright_Arzt == true && dbisactive == true)
                {
                    /// success , and status==true
                    return FunctionLoginStatus.SUCCESS_Arzt;
                }
                if (dbright_Schwester == true && dbisactive == true)
                {
                    /// success , and status==true
                    return FunctionLoginStatus.SUCCESS_Schwester;
                }
                if (dbright_Reinigungspersonal == true && dbisactive == true)
                {
                    /// success , and status==true
                    return FunctionLoginStatus.SUCCESS_Reinigungspersonal;
                }
                if (dbright_Therapeut == true && dbisactive == true)
                {
                    /// success , and status==true
                    return FunctionLoginStatus.SUCCESS_Therapeut;
                }

                else
                {
                    /// success , and status==false
                    return FunctionLoginStatus.FIRED;
                }
            }
        }

        /**  public ActionResult About()
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
          } **/


        /**public ActionResult Test2()
        {
            ViewBag.Message = "Another TestPage, for Rooms!";
            return View();
        }

        public ActionResult Test3()
        {
            ViewBag.Message = "Another TestPage, for Patients!";
            return View();
        } **/
    }
}