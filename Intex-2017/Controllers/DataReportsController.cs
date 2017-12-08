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
    public class DataReportsController : Controller
    {
        private IntexContext db = new IntexContext();
        // GET: DataReports
        [Authorize(Roles = "SysAdmin, Reports, TechDirector")]
        public ActionResult DataReportIndex()
        {
            List<DataReport> dataReportList = new List<DataReport>();
            dataReportList = db.DataReports.ToList();

            List<DataReport> assaysNeedingDataReports = new List<DataReport>();

            foreach (DataReport dr in dataReportList)
            {
                if (dr.DataReportPath == null)
                {
                    assaysNeedingDataReports.Add(dr);
                }
            }

            List<ReportsAssayIndexViewModel> viewModelList = new List<ReportsAssayIndexViewModel>();

            foreach (DataReport dr in assaysNeedingDataReports)
            {
                ReportsAssayIndexViewModel viewModel = new ReportsAssayIndexViewModel();
                viewModel.DataReportID = dr.DataReportID;
                viewModel.AssayID = dr.AssayID;
                viewModel.WorkOrderID = db.Assays.Find(dr.AssayID).WorkOrderID;
                viewModel.CustCompany = db.Customers.Find(db.WorkOrders.Find(db.Assays.Find(dr.AssayID).WorkOrderID).CustID).CustCompany;
                viewModel.CustFirstName = db.Customers.Find(db.WorkOrders.Find(db.Assays.Find(dr.AssayID).WorkOrderID).CustID).CustFirstName;
                viewModel.CustLastName = db.Customers.Find(db.WorkOrders.Find(db.Assays.Find(dr.AssayID).WorkOrderID).CustID).CustLastName;
                viewModel.LTNumber = db.Assays.Find(dr.AssayID).LTNumber;
                viewModel.DueDate = db.WorkOrders.Find(db.Assays.Find(dr.AssayID).WorkOrderID).DateDue;
                viewModelList.Add(viewModel);
            }
            return View(viewModelList);
        }

        [Authorize(Roles = "SysAdmin, Reports, TechDirector")]
        public ActionResult UploadReport(int? DataReportID)
        {
            DataReport dr = db.DataReports.Find(DataReportID);
            ViewBag.AssayNo = dr.AssayID;
            ViewBag.DataReportID = dr.DataReportID;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SysAdmin, Reports, TechDirector")]
        public ActionResult UploadReport(HttpPostedFileBase UploadedFile, FormCollection form)
        {
            // parse incoming form
            int DataReportID = Int32.Parse(form["DataReportID"]);
            DataReport postBackDR = db.DataReports.Find(DataReportID);
            ViewBag.AssayNo = postBackDR.AssayID;
            if (UploadedFile != null)
            {
                if (UploadedFile.ContentLength > 0)
                {
                    if (Path.GetExtension(UploadedFile.FileName) == ".pdf")
                    {
                        DataReport dr = db.DataReports.Find(DataReportID);
                        int? AssayNumber = dr.AssayID;
                        string fileName = "DataReport_Assay" + AssayNumber + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + ".pdf";
                        string folderPath = Path.Combine(Server.MapPath("~/UploadedFiles/DataReports"), fileName);

                        // save path to server
                        dr.DataReportPath = folderPath;
                        db.Entry(dr).State = EntityState.Modified;
                        db.SaveChanges();
                        UploadedFile.SaveAs(folderPath);
                        ViewBag.Message = "File Uploaded Successfully.";
                        // done saving to folder
                        return RedirectToAction("FileUploadSuccess", "DataReports", new { AssayID = dr.AssayID });
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
            return View(DataReportID);
        }

        [Authorize(Roles = "SysAdmin, Reports, TechDirector")]
        public ActionResult FileUploadSuccess(int? AssayID)
        {
            List<WorkOrder> woList = new List<WorkOrder>();
            List<Assay> assayList = new List<Assay>();
            assayList = db.Assays.ToList();
            List<DataReport> drList = new List<DataReport>();

            Assay a = db.Assays.Find(AssayID);
            List<Assay> assaysInThisWorkOrder = new List<Assay>();

            foreach (Assay assay in assayList)
            {
                if (assay.WorkOrderID == a.WorkOrderID)
                {
                    assaysInThisWorkOrder.Add(assay);
                }
            }
            bool AllDRAreFinished = true;
            foreach (Assay assay in assaysInThisWorkOrder)
            {
                var dr = db.DataReports.Where(x => x.AssayID == assay.AssayID).FirstOrDefault();
                if (dr == null)
                {
                    AllDRAreFinished = false;
                }
                else
                {
                    if (dr.DataReportPath == null)
                    {
                        AllDRAreFinished = false;
                    }
                }
            }
            
            if (AllDRAreFinished)
            {
                SummaryReport sr = new SummaryReport();
                sr.WorkOrderID = a.WorkOrderID;
                db.SummaryReports.Add(sr);
                db.SaveChanges();
            }

            WorkOrder wo = db.WorkOrders.Find(a.WorkOrderID);

            ViewBag.AssayID = AssayID;
            return View();
        }

        [Authorize(Roles = "Customer, SysAdmin, Manager")]
        public ActionResult GetDataReportPDF(String DataReportPath)
        {
            return File(DataReportPath, "application/pdf");
        }

        [Authorize(Roles = "SysAdmin, Manager")]
        public ActionResult ManagerView()
        {
            return View();
        }
    }
}