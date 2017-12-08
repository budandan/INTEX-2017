using Intex_2017.Models;
using Intex_2017.DAL;
using Intex_2017.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity;

namespace Intex_2017.Controllers
{
    [Authorize]
    public class SummaryReportsController : Controller
    {
        private IntexContext db = new IntexContext();
        // GET: SummaryReports
        [Authorize(Roles = "SysAdmin, TechDirector")]
        public ActionResult SummaryReportIndex()
        {
            List<SummaryReport> srList = new List<SummaryReport>();
            srList = db.SummaryReports.ToList();

            List<SummaryReport> srListNeedingUpload = new List<SummaryReport>();

            foreach (SummaryReport sr in srList)
            {
                if (sr.SummaryReportPath == null)
                {
                    srListNeedingUpload.Add(sr);
                }
            }

            List<TechDirectorSummaryReportsIndexViewModel> viewModelList = new List<TechDirectorSummaryReportsIndexViewModel>();

            foreach (SummaryReport sr in srListNeedingUpload)
            {
                TechDirectorSummaryReportsIndexViewModel viewModel = new TechDirectorSummaryReportsIndexViewModel();
                viewModel.WorkOrderID = sr.WorkOrderID;
                viewModel.SummaryReportID = sr.SummaryReportID;
                viewModel.CustCompany = db.Customers.Find(db.WorkOrders.Find(sr.WorkOrderID).CustID).CustCompany;
                viewModel.CustFirstName = db.Customers.Find(db.WorkOrders.Find(sr.WorkOrderID).CustID).CustFirstName;
                viewModel.CustLastName = db.Customers.Find(db.WorkOrders.Find(sr.WorkOrderID).CustID).CustLastName;
                viewModel.DateDue = db.WorkOrders.Find(sr.WorkOrderID).DateDue;
                viewModelList.Add(viewModel);
            }

            return View(viewModelList);
        }

        [Authorize(Roles = "SysAdmin, TechDirector")]
        public ActionResult UploadReport(int? SummaryReportID)
        {
            SummaryReport sr = db.SummaryReports.Find(SummaryReportID);
            ViewBag.WorkOrderID = sr.WorkOrderID;
            ViewBag.SummaryReportID = sr.SummaryReportID;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SysAdmin, TechDirector")]
        public ActionResult UploadReport(HttpPostedFileBase UploadedFile, FormCollection form)
        {
            // parse incoming form
            int SummaryReportID = Int32.Parse(form["SummaryReportID"]);
            SummaryReport postBackSR = db.SummaryReports.Find(SummaryReportID);
            ViewBag.WorkOrderID = postBackSR.WorkOrderID;
            if (UploadedFile != null)
            {
                if (UploadedFile.ContentLength > 0)
                {
                    if (Path.GetExtension(UploadedFile.FileName) == ".pdf")
                    {
                        SummaryReport sr = db.SummaryReports.Find(SummaryReportID);
                        int? WorkOrderID = sr.WorkOrderID;
                        string fileName = "SummaryReport_WorkOrder" + WorkOrderID + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + ".pdf";
                        string folderPath = Path.Combine(Server.MapPath("~/UploadedFiles/SummaryReports"), fileName);

                        // save path to server
                        sr.SummaryReportPath = folderPath;
                        db.Entry(sr).State = EntityState.Modified;
                        db.SaveChanges();
                        UploadedFile.SaveAs(folderPath);
                        ViewBag.Message = "File Uploaded Successfully.";
                        // done saving to folder
                        return RedirectToAction("FileUploadSuccess", "SummaryReports", new { WorkOrderID = sr.WorkOrderID });
                    }
                    else
                    {
                        ViewBag.Message = "Extension not supported.";
                    }
                }
            }
            else
            {
                ViewBag.Message = "File not selected.";
            }
            return View(SummaryReportID);
        }

        [Authorize(Roles = "SysAdmin, TechDirector")]
        public ActionResult FileUploadSuccess(int? WorkOrderID)
        {
            // fire off flag for invoice
            WorkOrder wo = db.WorkOrders.Find(WorkOrderID);
            wo.IsCompleted = true;
            db.Entry(wo).State = EntityState.Modified;
            db.SaveChanges();

            Invoice i = new Invoice();
            i.WorkOrderID = wo.WorkOrderID;
            db.Invoices.Add(i);
            db.SaveChanges();

            return View();
        }

        [Authorize(Roles = "Customer, SysAdmin, Manager")]
        public ActionResult GetSummaryReportPDF(String SummaryReportPath)
        {
            return File(SummaryReportPath, "application/pdf");
        }

        [Authorize(Roles = "SysAdmin, Manager")]
        public ActionResult ManagerView()
        {
            List<SummaryReport> srList = new List<SummaryReport>();
            srList = db.SummaryReports.ToList();

            List<SummaryReport> completedSummaryReports = new List<SummaryReport>();

            foreach (SummaryReport sr in srList)
            {
                if (sr.SummaryReportPath != null)
                {
                    completedSummaryReports.Add(sr);
                }
            }

            List<ManagerSummaryReportsViewModel> viewModelList = new List<ManagerSummaryReportsViewModel>();

            foreach (SummaryReport sr in completedSummaryReports)
            {
                ManagerSummaryReportsViewModel viewModel = new ManagerSummaryReportsViewModel();
                viewModel.WorkOrderID = db.WorkOrders.Find(sr.WorkOrderID).WorkOrderID;
                viewModel.LTNumber = db.Compounds.Find(db.WorkOrders.Find(sr.WorkOrderID).LTNumber).LTNumber;
                viewModel.CompoundName = db.Compounds.Find(db.WorkOrders.Find(sr.WorkOrderID).LTNumber).CompoundName;
                viewModel.CustFirstName = db.Customers.Find(db.WorkOrders.Find(sr.WorkOrderID).CustID).CustFirstName;
                viewModel.CustLastName = db.Customers.Find(db.WorkOrders.Find(sr.WorkOrderID).CustID).CustLastName;
                viewModel.CustCompany = db.Customers.Find(db.WorkOrders.Find(sr.WorkOrderID).CustID).CustCompany;
                viewModel.SummaryReportPath = sr.SummaryReportPath;
                viewModelList.Add(viewModel);
            }

            return View(viewModelList);
        }
    }
}