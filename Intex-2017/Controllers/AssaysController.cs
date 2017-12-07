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
    [Authorize]
    public class AssaysController : Controller
    {
        private IntexContext db = new IntexContext();

        [Authorize(Roles = "SysAdmin, LabTech")]
        public ActionResult ScheduleAssays()
        {
            List<Assay> assayList = new List<Assay>();
            assayList = db.Assays.ToList();

            List<Assay> assaysNoStartDate = new List<Assay>();

            foreach (Assay a in assayList)
            {
                if (a.StartDate == null)
                {
                    assaysNoStartDate.Add(a);
                }
            }

            List<LabTechScheduleAssaysViewModel> viewModelList = new List<LabTechScheduleAssaysViewModel>();

            foreach (Assay a in assaysNoStartDate)
            {
                LabTechScheduleAssaysViewModel viewModel = new LabTechScheduleAssaysViewModel();
                viewModel.AssayID = a.AssayID;
                viewModel.WorkOrderID = a.WorkOrderID;
                viewModel.AssayNameDesc = db.AssayNames.Find(a.AssayNameID).AssayNameDesc;
                viewModel.ClientQuantity = db.WorkOrders.Find(a.WorkOrderID).ClientQuantity;
                viewModel.CompoundName = db.Compounds.Find(a.LTNumber).CompoundName;
                viewModelList.Add(viewModel);
            }

            return View(viewModelList);
        }

        public ActionResult ScheduleOneAssay(int? AssayID)
        {


            return View();
        }

        // GET: Assays
        public ActionResult Index()
        {
            return View(db.Assays.ToList());
        }

        // GET: Assays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assay assay = db.Assays.Find(id);
            if (assay == null)
            {
                return HttpNotFound();
            }
            return View(assay);
        }

        // GET: Assays/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Assays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssayID,AssayNameID,WorkOrderID,Protocol,EstDuration,StatusID,QuantResults,QualResults,WorkHoursReq,StartDate,LTNumber,IsRequired,Cost")] Assay assay)
        {
            if (ModelState.IsValid)
            {
                db.Assays.Add(assay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assay);
        }

        // GET: Assays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assay assay = db.Assays.Find(id);
            if (assay == null)
            {
                return HttpNotFound();
            }
            return View(assay);
        }

        // POST: Assays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssayID,AssayNameID,WorkOrderID,Protocol,EstDuration,StatusID,QuantResults,QualResults,WorkHoursReq,StartDate,LTNumber,IsRequired,Cost")] Assay assay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assay);
        }

        // GET: Assays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assay assay = db.Assays.Find(id);
            if (assay == null)
            {
                return HttpNotFound();
            }
            return View(assay);
        }

        // POST: Assays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assay assay = db.Assays.Find(id);
            db.Assays.Remove(assay);
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
