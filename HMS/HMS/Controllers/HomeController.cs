using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.SqlClient;

namespace HMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult LoginPage()
        //{

        //    return View();
        //}

        [HttpPost]
        public ActionResult Index(String username, String password)
        {
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
                case FunctionLoginStatus.SUCCESS1:
                    result = View("Home", "_Layout1");
                    break;
                case FunctionLoginStatus.SUCCESS2:
                    result = View("Home", "_Layout2");
                    break;
                case FunctionLoginStatus.SUCCESS3:
                    result = View("Home", "_Layout3");
                    break;
                case FunctionLoginStatus.SUCCESS4:
                    result = View("Home", "_Layout4");
                    break;
                case FunctionLoginStatus.FAIL:
                    ViewBag.LoginMessage = "Something wrong! Try Again!";
                    break;
                case FunctionLoginStatus.FIRED:
                    ViewBag.LoginMessage = "You are fired or retired! Contact your boss!";
                    break;
                //case FunctionLoginStatus.RETIRE:
                //    ViewBag.LoginMessage = "You are retired! goodbye!";
                //    break;
                default:
                    break;
            }

            return result;
        }

        public enum FunctionLoginStatus
        {
            SUCCESS1, SUCCESS2, SUCCESS3, SUCCESS4,
            FAIL,
            FIRED,
            RETIRE
        }

        public FunctionLoginStatus FunctionLogin(String username, String password)
        {
            SqlConnection connection = null;

            String ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=HMSDB;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            connection = new SqlConnection(ConnectionString);

            String SQLString = String.Format("SELECT username,rolename,accessright1,accessright2,accessright3,accessright4,accessright5 FROM dbo.ObjectSet_User WHERE username = '{0}' AND password = '{1}'", username, password);

            SqlCommand cmd = new SqlCommand(SQLString, connection);

            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            String dbUsername = null;
            Boolean dbright1 = false;
            Boolean dbright2 = false;
            Boolean dbright3 = false;
            Boolean dbright4 = false;
            Boolean dbstatus = false;

            while (reader.Read() == true)
            {
                dbUsername = Convert.ToString(reader["username"]);
                /// ......
                dbright1 = Convert.ToBoolean(reader["accessright1"]);
                dbright2 = Convert.ToBoolean(reader["accessright2"]);
                dbright3 = Convert.ToBoolean(reader["accessright3"]);
                dbright4 = Convert.ToBoolean(reader["accessright4"]);
                dbstatus = Convert.ToBoolean(reader["accessright5"]);
                // so far use Recht5 instead status
            }

            if (dbUsername == null)
            {
                /// fail
                return FunctionLoginStatus.FAIL;
            }
            else
            {
                /// success
                if (dbright1 == true && dbstatus == true ) 
                {
                    /// success , and status==true
                    return FunctionLoginStatus.SUCCESS1;
                }
                if (dbright2 == true && dbstatus == true)
                {
                    /// success , and status==true
                    return FunctionLoginStatus.SUCCESS2;
                }
                if (dbright3 == true && dbstatus == true)
                {
                    /// success , and status==true
                    return FunctionLoginStatus.SUCCESS3;
                }
                if (dbright4 == true && dbstatus == true)
                {
                    /// success , and status==true
                    return FunctionLoginStatus.SUCCESS4;
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