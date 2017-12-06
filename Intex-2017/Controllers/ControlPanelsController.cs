using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intex_2017.Controllers
{
    [Authorize]
    public class ControlPanelsController : Controller
    {
        // GET: ControlPanels
        [Authorize(Roles = "SysAdmin, SalesAgent")]
        public ActionResult Index()
        {
            if (User.IsInRole("SysAdmin"))
            {
                return RedirectToAction("SysAdmin", "ControlPanels");
            }
            else if (User.IsInRole("SalesAgent"))
            {
                return RedirectToAction("SalesAgent", "ControlPanels");
            }
            return View();
        }

        [Authorize(Roles = "SysAdmin")]
        public ActionResult SysAdmin()
        {
            return View();
        }

        [Authorize(Roles = "SalesAgent")]
        public ActionResult SalesAgent()
        {
            return View();
        }
    }
}