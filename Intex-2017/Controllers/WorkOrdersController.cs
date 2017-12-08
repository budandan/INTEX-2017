using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Intex_2017.DAL;
using Intex_2017.Models;
using Intex_2017.Models.ViewModels;

namespace Intex_2017.Controllers
{
    [Authorize]
    public class WorkOrdersController : Controller
    {
        
        private IntexContext db = new IntexContext();
        [Authorize(Roles = "SysAdmin, SalesAgent")]
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
        [Authorize(Roles = "SysAdmin, SalesAgent")]
        public ActionResult SelectAssays(FormCollection form)  
        {
            String LTNumber = form["LTNumber"];
            int key = Int32.Parse(LTNumber);
            String CustID = form["CustID"];
            String SalesAgentName = form["SalesAgentName"];

            Compound c = db.Compounds.Find(key);
            List<AssayName> assayList = new List<AssayName>();
            assayList = db.AssayNames.ToList();

            ViewBag.assayList = assayList;

            PreWorkOrderViewModel viewModel = new PreWorkOrderViewModel();
            if (CustID != "")
            {
                viewModel.CustID = Convert.ToInt32(CustID);
            }
            viewModel.CompoundName = c.CompoundName;
            viewModel.SalesAgentName = SalesAgentName;
            viewModel.LTNumber = key;

            return View(viewModel);
        }
        [HttpPost]
        [Authorize(Roles = "SysAdmin, SalesAgent")]
        public ActionResult Quote([Bind(Include = "CompoundName,CustID,LTNumber,SalesAgentName,BiochemicalPharmacology,DiscoveryScreen,ImmunoScreen,ProfilingScreen,PharmaScreen,CustomScreen")] PreWorkOrderViewModel viewModel)
        {
            //List<Assay> assayList = new List<Assay>();
            //assayList = new List<Assay>();
            //assayList = db.Assays.ToList();

            //List<Assay> completedRequiredAssays = new List<Assay>();

            //List<int> selectedAssayIDs = new List<int>();
            //if (viewModel.BiochemicalPharmacology)
            //{
            //    selectedAssayIDs.Add(1);
            //}
            //if (viewModel.BiochemicalPharmacology)
            //{
            //    selectedAssayIDs.Add(2);
            //}
            //if (viewModel.DiscoveryScreen)
            //{
            //    selectedAssayIDs.Add(3);
            //}
            //if (viewModel.ImmunoScreen)
            //{
            //    selectedAssayIDs.Add(4);
            //}
            //if (viewModel.PharmaScreen)
            //{
            //    selectedAssayIDs.Add(5);
            //}
            //if (viewModel.CustomScreen)
            //{
            //    selectedAssayIDs.Add(6);
            //}

            //// to be added in the calculation for price quote, the assay needs to contain an int AssayNameID, LTNumber and needs to be required
            //foreach (Assay a in assayList)
            //{
            //    if (a.IsRequired == true && a.LTNumber == viewModel.LTNumber && selectedAssayIDs.Contains(a.AssayID))
            //    {
            //        completedRequiredAssays.Add(a);
            //    }
            //}

            //List<int> numberOfAssaysInCombo = new List<int>();
            //List<decimal> runningCostOfAssaysInCombo = new List<decimal>();
            //List<decimal> averages = new List<decimal>();
            //if (completedRequiredAssays.Count == 0)
            //{
            //    ViewBag.Price = "Unable to find a quote. Insufficient data in database.";
            //}
            //else
            //{
            //    for (int i = 0; i < selectedAssayIDs.Count; i++)
            //    {
            //        // average each LT/AssayNameID combo
            //        numberOfAssaysInCombo[i] = 0;
            //        foreach (Assay a in completedRequiredAssays)
            //        {
            //            if (a.AssayNameID == selectedAssayIDs[i])
            //            {
            //                numberOfAssaysInCombo[i]++;
            //                runningCostOfAssaysInCombo[i] += a.Cost;
            //            }
            //        }
            //        averages[i] = runningCostOfAssaysInCombo[i] / numberOfAssaysInCombo[i];
            //    }
            //    // sum all values in averages
            //    decimal quotedPrice = 0;
            //    foreach (decimal avg in averages)
            //    {
            //        quotedPrice += avg;
            //    }
            //    ViewBag.quotedPrice = quotedPrice;
            //}

            ViewBag.quotedPrice = "[Quoted Price]";

            return View(viewModel);
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
        [Authorize(Roles = "SysAdmin, SalesAgent")]
        public ActionResult Create(int LTNumber, int CustID, String SalesAgentName, bool a1, bool a2, bool a3, bool a4, bool a5, bool a6)
        {
            ViewBag.CustID = CustID;
            ViewBag.LTNumber = LTNumber;

            var SalesAgentID = db.Database.SqlQuery<int>(
                "SELECT Employee.EmployeeID FROM Employee WHERE Employee.EmpUsername = \'" + User.Identity.Name + "\'"
                ).FirstOrDefault();

            ViewBag.SalesAgentID = SalesAgentID;
            ViewBag.a1 = a1;
            ViewBag.a2 = a2;
            ViewBag.a3 = a3;
            ViewBag.a4 = a4;
            ViewBag.a5 = a5;
            ViewBag.a6 = a6;
            return View();
        }

        // POST: WorkOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SysAdmin, SalesAgent")]
        public ActionResult Create([Bind(Include = "WorkOrderID,Comments,ClientQuantity,DateDue,CustID,SalesAgentID,LTNumber")] WorkOrder workOrder, FormCollection form)
        {
            String a1 = form["a1"];
            String a2 = form["a2"];
            String a3 = form["a3"];
            String a4 = form["a4"];
            String a5 = form["a5"];
            String a6 = form["a6"];

            if (ModelState.IsValid)
            {
                db.WorkOrders.Add(workOrder);
                db.SaveChanges();
                Assay assay = new Assay();
                if (a1 == "True")
                {
                    //Assay assay = new Assay();
                    assay.AssayNameID = 1;
                    assay.WorkOrderID = workOrder.WorkOrderID;
                    assay.StatusID = 1;
                    assay.LTNumber = workOrder.LTNumber;
                    db.Assays.Add(assay);
                    db.SaveChanges();
                }
                if (a2 == "True")
                {
                    // Assay assay = new Assay();
                    assay.AssayNameID = 2;
                    assay.WorkOrderID = workOrder.WorkOrderID;
                    assay.StatusID = 1;
                    assay.LTNumber = workOrder.LTNumber;
                    db.Assays.Add(assay);
                    db.SaveChanges();
                }
                if (a3 == "True")
                {
                    //Assay assay = new Assay();
                    assay.AssayNameID = 3;
                    assay.WorkOrderID = workOrder.WorkOrderID;
                    assay.StatusID = 1;
                    assay.LTNumber = workOrder.LTNumber;
                    db.Assays.Add(assay);
                    db.SaveChanges();
                }
                if (a4 == "True")
                {
                    // Assay assay = new Assay();
                    assay.AssayNameID = 4;
                    assay.WorkOrderID = workOrder.WorkOrderID;
                    assay.StatusID = 1;
                    assay.LTNumber = workOrder.LTNumber;
                    db.Assays.Add(assay);
                    db.SaveChanges();
                }
                if (a5 == "True")
                {
                    //Assay assay = new Assay();
                    assay.AssayNameID = 5;
                    assay.WorkOrderID = workOrder.WorkOrderID;
                    assay.StatusID = 1;
                    assay.LTNumber = workOrder.LTNumber;
                    db.Assays.Add(assay);
                    db.SaveChanges();
                }
                if (a6 == "True")
                {

                    assay.AssayNameID = 6;
                    assay.WorkOrderID = workOrder.WorkOrderID;
                    assay.StatusID = 1;
                    assay.LTNumber = workOrder.LTNumber;
                    db.Assays.Add(assay);
                    db.SaveChanges();
                }
                ViewBag.CustID = workOrder.CustID;
                return RedirectToAction("ConfirmWorkOrder", "WorkOrders", new { id = workOrder.WorkOrderID });
            }

            return View(workOrder);
        }
        [Authorize(Roles = "Customer, SysAdmin")]
        public ActionResult CustomerViewIndex(int? id)
        {
            List<WorkOrder> workOrderList = new List<WorkOrder>();
            workOrderList = db.WorkOrders.ToList();

            List<CustomerSeeWorkOrdersViewModel> viewModelList = new List<CustomerSeeWorkOrdersViewModel>();
            foreach (WorkOrder wo in workOrderList)
            {
                if (wo.CustID == id)
                {
                    CustomerSeeWorkOrdersViewModel viewModel = new CustomerSeeWorkOrdersViewModel();
                    viewModel.WorkOrderID = wo.WorkOrderID;
                    viewModel.DateDue = wo.DateDue;
                    viewModel.CompoundName = db.Compounds.Find(wo.LTNumber).CompoundName;
                    viewModel.IsVerified = wo.IsVerified;
                    var i = db.Invoices.Where(x => x.WorkOrderID == wo.WorkOrderID).FirstOrDefault();
                    if (i != null)
                    {
                        if (i.InvoicePath != null)
                        {
                            viewModel.HasInvoice = true;
                            viewModel.InvoicePath = i.InvoicePath;
                            viewModel.InvoiceID = i.InvoiceID;
                            if (i.IsPaid == true)
                            {
                                viewModel.InvoicePaid = true;
                            }
                            else
                            {
                                viewModel.InvoicePaid = false;
                            }
                        }
                        else
                        {
                            viewModel.HasInvoice = false;
                        }
                    }
                    else
                    {
                        viewModel.HasInvoice = false;
                    }
                    viewModelList.Add(viewModel);
                }
            }
            return View(viewModelList);
        }
        [Authorize(Roles = "Customer, SysAdmin")]
        public ActionResult CustomerViewAssays(int? id, String compound)
        {
            List<Assay> assayList = new List<Assay>();
            assayList = db.Assays.ToList();
            ViewBag.HasASummary = false;
            ViewBag.SummmaryReportPath = null;
            List<CustomerViewAssaysViewModel> viewModelList = new List<CustomerViewAssaysViewModel>();
            foreach (Assay a in assayList)
            {
                if (a.WorkOrderID == id)
                {
                    CustomerViewAssaysViewModel viewModel = new CustomerViewAssaysViewModel();
                    viewModel.AssayNameDesc = db.AssayNames.Find(a.AssayNameID).AssayNameDesc;
                    viewModel.StatusName = db.Statuses.Find(a.StatusID).StatusName;
                    var dataReport = db.DataReports.Where(x => x.AssayID == a.AssayID).FirstOrDefault();
                    if (dataReport != null)
                    {
                        viewModel.DataReportPath = dataReport.DataReportPath;
                    }
                    var summaryReport = db.SummaryReports.Where(x => x.WorkOrderID == a.WorkOrderID).FirstOrDefault();
                    if (summaryReport != null)
                    {
                        viewModel.SummaryReportPath = summaryReport.SummaryReportPath;
                        ViewBag.HasASummary = true;
                        ViewBag.SummaryReportPath = summaryReport.SummaryReportPath;
                    }
                    viewModelList.Add(viewModel);
                }
            }
            ViewBag.WorkOrderNo = id;
            ViewBag.compound = compound; // set compound name for the view
            return View(viewModelList);
        }

        public ActionResult ConfirmWorkOrder(int id)
        {
            ViewBag.id = id;
            return View();
        }

        [Authorize(Roles = "SysAdmin, Billing")]
        public ActionResult VerifyCreditIndex()
        {
            List<WorkOrder> workOrderList = new List<WorkOrder>();
            workOrderList = db.WorkOrders.ToList();

            List<WorkOrder> workOrderListToModify = new List<WorkOrder>();
            workOrderListToModify = db.WorkOrders.ToList();
            foreach (WorkOrder wo in workOrderList)
            {
                if (wo.IsVerified == true)
                {
                    workOrderListToModify.Remove(wo);
                }
            }

            List<VerifyCreditViewModel> viewModelList = new List<VerifyCreditViewModel>();
            foreach (WorkOrder wo in workOrderListToModify)
            {
                VerifyCreditViewModel viewModel = new VerifyCreditViewModel();
                viewModel.WorkOrderID = wo.WorkOrderID;
                viewModel.CustCompany = db.Customers.Find(wo.CustID).CustCompany;
                viewModel.CustFirstName = db.Customers.Find(wo.CustID).CustFirstName;
                viewModel.CustLastName = db.Customers.Find(wo.CustID).CustLastName;
                viewModelList.Add(viewModel);
            }
            return View(viewModelList);
        }

        [Authorize(Roles = "SysAdmin, Billing")]
        public ActionResult VerifyCredit(int? id)
        {
            WorkOrder wo = db.WorkOrders.Find(id);
            VerifyCreditViewModel viewModel = new VerifyCreditViewModel();
            viewModel.WorkOrderID = db.WorkOrders.Find(id).WorkOrderID;
            viewModel.CustCompany= db.Customers.Find(wo.CustID).CustCompany;
            viewModel.CustFirstName = db.Customers.Find(wo.CustID).CustFirstName;
            viewModel.CustLastName = db.Customers.Find(wo.CustID).CustLastName;
            return View(viewModel);
        }

        [Authorize(Roles = "SysAdmin, Billing")]
        public ActionResult VerifyCreditConfirm(int? id)
        {
            WorkOrder wo = db.WorkOrders.Find(id);
            wo.IsVerified = true;
            db.Entry(wo).State = EntityState.Modified;
            db.SaveChanges();
            return View();
        }

        [Authorize(Roles = "SysAdmin, LabTech")]
        public ActionResult ReceiveCompound()
        {
            List<WorkOrder> workOrderList = new List<WorkOrder>();
            workOrderList = db.WorkOrders.ToList();

            List<WorkOrder> workOrderListNotReceived = new List<WorkOrder>();

            // get all work orders that have not been received
            foreach (WorkOrder wo in workOrderList)
            {
                if (wo.IsConfirmed == false)
                {
                    workOrderListNotReceived.Add(wo);
                }
            }

            List<LabTechReceiveCompoundViewModel> viewModelList = new List<LabTechReceiveCompoundViewModel>();

            foreach (WorkOrder wo in workOrderListNotReceived)
            {
                LabTechReceiveCompoundViewModel viewModel = new LabTechReceiveCompoundViewModel();
                viewModel.WorkOrderID = wo.WorkOrderID;
                viewModel.CustFirstName = db.Customers.Find(wo.CustID).CustFirstName;
                viewModel.CustLastName = db.Customers.Find(wo.CustID).CustLastName;
                viewModel.CompoundName = db.Compounds.Find(wo.LTNumber).CompoundName;
                viewModelList.Add(viewModel);
            }

            return View(viewModelList);
        }

        [Authorize(Roles = "SysAdmin, LabTech")]
        public ActionResult WeighCompound(int woID)
        {
            WorkOrder wo = db.WorkOrders.Find(woID);
            ViewBag.ClientQuantity = wo.ClientQuantity;
            ViewBag.WorkOrderID = woID;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SysAdmin, LabTech")]
        public ActionResult MarkAsReceived(FormCollection form)
        {
            // parse form
            int woID = Int32.Parse(form["woID"]);
            decimal ActualQuantity = Decimal.Parse(form["Mass"]);

            // mark work order as received, override mass and say who received it
            WorkOrder wo = new WorkOrder();
            wo = db.WorkOrders.Find(woID);
            wo.IsConfirmed = true; // mark as received
            wo.ActualQuantity = ActualQuantity; // denote actual quantity
            var employee = db.Employees.Where(x => x.EmpUsername == User.Identity.Name).FirstOrDefault();
            if (employee != null)
            {
                wo.ReceivedByWho = employee.EmployeeID;
            }
            db.Entry(wo).State = EntityState.Modified;
            db.SaveChanges();

            // mark all of those work order's assays as received
            List<Assay> assayList = new List<Assay>();
            assayList = db.Assays.ToList();

            List<Assay> workOrderAssays = new List<Assay>();

            foreach (Assay a in assayList)
            {
                if (a.WorkOrderID == woID)
                {
                    workOrderAssays.Add(a);
                }
            }

            foreach (Assay a in workOrderAssays)
            {
                a.StatusID = 2;
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.woID = woID;
            return View();
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
