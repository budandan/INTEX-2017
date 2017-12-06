using Intex_2017.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intex_2017.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        private IntexContext db = new IntexContext();
        public ActionResult Index()
        {
            if (User.IsInRole("SysAdmin"))
            {
                return RedirectToAction("SysAdmin", "Dashboard");
            }
            else if (User.IsInRole("LabTech"))
            {
                return RedirectToAction("LabTech", "Dashboard");
            }
            else if (User.IsInRole("SalesAgent"))
            {
                return RedirectToAction("SalesAgent", "Dashboard");
            }
            else if (User.IsInRole("Billing"))
            {
                return RedirectToAction("Billing", "Dashboard");
            }
            return View();
        }

        [Authorize(Roles = "SysAdmin")]
        public ActionResult SysAdmin()
        {
            return View();
        }

        [Authorize(Roles = "Billing, SysAdmin")]
        public ActionResult Billing()
        {
            return View();
        }

        [Authorize(Roles = "SalesAgent, SysAdmin")]
        public ActionResult SalesAgent()
        {
            db.con.Open();
            string query = "SELECT COUNT(*) FROM CallTicket WHERE IsOpen = 1";
            using (var cmd = new SqlCommand(query, db.con))
            {
                int rowsAmount = (int)cmd.ExecuteScalar(); // get the value of the count
                ViewBag.NoOfOpenTickets = rowsAmount;
            }
            
            return View();
        }

        [Authorize(Roles = "LabTech, SysAdmin")]
        public ActionResult LabTech()
        {
            return View();
        }

        [Authorize(Roles = "Customer, SysAdmin")]
        public ActionResult Customer()
        {
            return View();
        }
    }
}