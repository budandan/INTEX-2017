using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Intex_2017.DAL;
using Intex_2017.Models;
using Intex_2017.Models.ViewModels;

namespace Intex_2017.Controllers
{
    public class WorkOrdersController : Controller
    {
        private IntexContext db = new IntexContext();

        public ActionResult SetCompound(int? CustID, String SalesAgentName)
        {
            List<Compound> compoundList = new List<Compound>();
            compoundList = db.Compounds.ToList();

            ViewBag.CustID = CustID.ToString();
            ViewBag.SalesAgentName = SalesAgentName;
            ViewBag.compoundList = compoundList;

            return View();
        }

        [HttpPost]
        public ActionResult SelectAssays(FormCollection form)  
        {
            String LTNumber = form["LTNumber"];
            int key = Int32.Parse(LTNumber);
            String CustID = form["CustID"];
            String SalesAgentName = form["SalesAgentName"];

            Compound c = db.Compounds.Find(key);
            ViewBag.compoundName = c.CompoundName;
            ViewBag.CustID = CustID;
            ViewBag.SalesAgentName = SalesAgentName;

            return View();
        }
        // GET: WorkOrders
        public ActionResult Index()
        {
            return View(db.WorkOrders.ToList());
        }

        // GET: WorkOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrder = db.WorkOrders.Find(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            return View(workOrder);
        }

        // GET: WorkOrders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkOrderID,Comments,DateArrived,ClientQuantity,ReceivedByWho,DateDue,Weight,CustID,SalesAgentID,ActualQuantity,IsConfirmed,IsVerified")] WorkOrder workOrder)
        {
            if (ModelState.IsValid)
            {
                db.WorkOrders.Add(workOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workOrder);
        }

        // GET: WorkOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrder = db.WorkOrders.Find(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            return View(workOrder);
        }

        // POST: WorkOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkOrderID,Comments,DateArrived,ClientQuantity,ReceivedByWho,DateDue,Weight,CustID,SalesAgentID,ActualQuantity,IsConfirmed,IsVerified")] WorkOrder workOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workOrder);
        }

        // GET: WorkOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrder = db.WorkOrders.Find(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            return View(workOrder);
        }

        // POST: WorkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkOrder workOrder = db.WorkOrders.Find(id);
            db.WorkOrders.Remove(workOrder);
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
