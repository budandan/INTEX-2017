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
    public class InvoicesController : Controller
    {
        private IntexContext db = new IntexContext();
        // GET: Invoices
        [Authorize(Roles = "SysAdmin, Billing")]
        public ActionResult CreateInvoiceIndex()
        {
            List<Invoice> invoiceList = new List<Invoice>();
            invoiceList = db.Invoices.ToList();

            List<Invoice> woNeedingInvoiceUpload = new List<Invoice>();

            foreach (Invoice i in invoiceList)
            {
                if (i.InvoicePath == null)
                {
                    woNeedingInvoiceUpload.Add(i);
                }
            }

            List<BillingInvoiceIndexViewModel> viewModelList = new List<BillingInvoiceIndexViewModel>();

            foreach (Invoice i in woNeedingInvoiceUpload)
            {
                BillingInvoiceIndexViewModel viewModel = new BillingInvoiceIndexViewModel();
                viewModel.InvoiceID = i.InvoiceID;
                viewModel.WorkOrderID = i.WorkOrderID;
                viewModel.CustFirstName = db.Customers.Find(db.WorkOrders.Find(i.WorkOrderID).CustID).CustFirstName;
                viewModel.CustLastName = db.Customers.Find(db.WorkOrders.Find(i.WorkOrderID).CustID).CustLastName;
                viewModel.CustCompany = db.Customers.Find(db.WorkOrders.Find(i.WorkOrderID).CustID).CustCompany;
                viewModel.CompoundName = db.Compounds.Find(db.WorkOrders.Find(i.WorkOrderID).LTNumber).CompoundName;
                viewModelList.Add(viewModel);
            }

            return View(viewModelList);
        }

        [Authorize(Roles = "SysAdmin, Billing")]
        public ActionResult UploadInvoice(int? InvoiceID)
        {
            Invoice i = db.Invoices.Find(InvoiceID);
            ViewBag.WorkOrderID = i.WorkOrderID;
            ViewBag.InvoiceID = i.InvoiceID;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SysAdmin, Billing")]
        public ActionResult UploadInvoice(HttpPostedFileBase UploadedFile, FormCollection form)
        {
            // parse incoming form
            int InvoiceID = Int32.Parse(form["InvoiceID"]);
            Invoice postBackInvoice = db.Invoices.Find(InvoiceID);
            ViewBag.WorkOrderID = postBackInvoice.WorkOrderID;
            if (UploadedFile != null)
            {
                if (UploadedFile.ContentLength > 0)
                {
                    if (Path.GetExtension(UploadedFile.FileName) == ".pdf")
                    {
                        Invoice i = db.Invoices.Find(InvoiceID);
                        int? WorkOrderID = i.WorkOrderID;
                        string fileName = "Invoice_WorkOrderID" + WorkOrderID + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + ".pdf";
                        string folderPath = Path.Combine(Server.MapPath("~/UploadedFiles/Invoices"), fileName);

                        // save path to server
                        i.InvoicePath = folderPath;
                        db.Entry(i).State = EntityState.Modified;
                        db.SaveChanges();
                        UploadedFile.SaveAs(folderPath);
                        ViewBag.Message = "File Uploaded Successfully.";
                        // done saving to folder
                        return RedirectToAction("FileUploadSuccess", "Invoices", new { InvoiceID = i.InvoiceID });
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
            return View(InvoiceID);
        }

        [Authorize(Roles = "Billing, SysAdmin")]
        public ActionResult FileUploadSuccess(int? InvoiceID)
        {
            // Mark Work Order as completed
            Invoice i = db.Invoices.Find(InvoiceID);
            WorkOrder wo = db.WorkOrders.Find(i.WorkOrderID);
            wo.IsClosed = true;
            db.Entry(wo).State = EntityState.Modified;
            db.SaveChanges();

            return View();
        }

        [Authorize(Roles = "Customer, SysAdmin")]
        public ActionResult CustomerPayInvoice(int? InvoiceID)
        {
            Invoice i = db.Invoices.Find(InvoiceID);
            ViewBag.InvoiceID = InvoiceID;
            ViewBag.WorkOrderID = i.WorkOrderID;
            return View();
        }

        [Authorize(Roles = "Customer, SysAdmin")]
        public ActionResult PaymentSuccess(int? WorkOrderID, int? InvoiceID)
        {
            Invoice i = db.Invoices.Find(InvoiceID);
            i.IsPaid = true;
            db.Entry(i).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.WorkOrderID = WorkOrderID;
            return View();
        }

        [Authorize(Roles = "Customer, SysAdmin, Manager")]
        public ActionResult GetInvoicePDF(int? InvoiceID)
        {
            Invoice i = db.Invoices.Find(InvoiceID);
            return File(i.InvoicePath, "application/pdf");
        }

        [Authorize(Roles = "SysAdmin, Manager")]
        public ActionResult ManagerView()
        {
            List<Invoice> invoiceList = new List<Invoice>();
            invoiceList = db.Invoices.ToList();

            List<Invoice> paidInvoices = new List<Invoice>();

            foreach (Invoice i in invoiceList)
            {
                if (i.InvoicePath != null && i.IsPaid == true)
                {
                    paidInvoices.Add(i);
                }
            }

            List<ManagerInvoicesViewModel> viewModelList = new List<ManagerInvoicesViewModel>();

            foreach (Invoice i in paidInvoices)
            {
                ManagerInvoicesViewModel viewModel = new ManagerInvoicesViewModel();
                viewModel.WorkOrderID = db.WorkOrders.Find(i.WorkOrderID).WorkOrderID;
                viewModel.LTNumber = db.Compounds.Find(db.WorkOrders.Find(i.WorkOrderID).LTNumber).LTNumber;
                viewModel.CompoundName = db.Compounds.Find(db.WorkOrders.Find(i.WorkOrderID).LTNumber).CompoundName;
                viewModel.CustFirstName = db.Customers.Find(db.WorkOrders.Find(i.WorkOrderID).CustID).CustFirstName;
                viewModel.CustLastName = db.Customers.Find(db.WorkOrders.Find(i.WorkOrderID).CustID).CustLastName;
                viewModel.CustCompany = db.Customers.Find(db.WorkOrders.Find(i.WorkOrderID).CustID).CustCompany;
                viewModel.InvoiceID = i.InvoiceID;
                viewModelList.Add(viewModel);
            }

            return View(viewModelList);
        }
    }
}