using Intex_2017.Models;
using Intex_2017.DAL;
using Intex_2017.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult UploadReport()
        {
            return View();
        }
    }
}