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
using System.IO;

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
                viewModel.CustCompany = db.Customers.Find(db.WorkOrders.Find(a.WorkOrderID).CustID).CustCompany;
                viewModel.CustFirstName = db.Customers.Find(db.WorkOrders.Find(a.WorkOrderID).CustID).CustFirstName;
                viewModel.CustLastName = db.Customers.Find(db.WorkOrders.Find(a.WorkOrderID).CustID).CustLastName;
                viewModel.AssayNameDesc = db.AssayNames.Find(a.AssayNameID).AssayNameDesc;
                viewModel.DueDate = db.WorkOrders.Find(a.WorkOrderID).DateDue;
                viewModel.CompoundName = db.Compounds.Find(a.LTNumber).CompoundName;
                viewModelList.Add(viewModel);
            }

            return View(viewModelList);
        }

        public ActionResult ScheduleOneAssay(int? AssayID)
        {
            ViewBag.AssayID = AssayID;
            LabTechScheduleOneAssayViewModel viewModel = new LabTechScheduleOneAssayViewModel();

            Assay a = db.Assays.Find(AssayID);

            viewModel.AssayID = a.AssayID;
            viewModel.WorkOrderID = a.WorkOrderID;
            viewModel.CustCompany = db.Customers.Find(db.WorkOrders.Find(a.WorkOrderID).CustID).CustCompany;
            viewModel.CustFirstName = db.Customers.Find(db.WorkOrders.Find(a.WorkOrderID).CustID).CustFirstName;
            viewModel.CustLastName = db.Customers.Find(db.WorkOrders.Find(a.WorkOrderID).CustID).CustLastName;
            viewModel.AssayNameDesc = db.AssayNames.Find(a.AssayNameID).AssayNameDesc;
            viewModel.DueDate = db.WorkOrders.Find(a.WorkOrderID).DateDue;
            viewModel.CompoundName = db.Compounds.Find(a.LTNumber).CompoundName;
            viewModel.Comments = db.WorkOrders.Find(a.WorkOrderID).Comments;
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "SysAdmin, LabTech")]
        public ActionResult ConfirmScheduledAssay([Bind(Include = "StartDate")] Assay placeholderAssay, FormCollection form)
        {
            int AssayID = Int32.Parse(form["AssayID"]);
            int NoOfSamples = Int32.Parse(form["Samples"]);
            if (ModelState.IsValid)
            {
                Assay assay = db.Assays.Find(AssayID);
                assay.StartDate = placeholderAssay.StartDate;
                assay.StatusID = 3;

                ViewBag.AssayID = assay.AssayID;
                ViewBag.StartDate = assay.StartDate;

                // create a SampleTest row in SampleTest table for each sample
                for (int i = 1; i <= NoOfSamples; i++)
                {
                    SampleTest sampleTest = new SampleTest();
                    sampleTest.LTNumber = assay.LTNumber;
                    sampleTest.AssayID = assay.AssayID;
                    sampleTest.CompoundSeqCode = i;
                    db.SampleTests.Add(sampleTest);
                    db.SaveChanges();
                }

                db.Entry(assay).State = EntityState.Modified;
                db.SaveChanges();
                return View();
            }
            return View();
        }

        [Authorize(Roles = "SysAdmin, LabTech")]
        public ActionResult SubmitResultsIndex()
        {
            List<Assay> assayList = new List<Assay>();
            assayList = db.Assays.ToList();
            List<Assay> scheduledAssays = new List<Assay>();

            List<LabTechSubmitAssayResultsViewModel> viewModelList = new List<LabTechSubmitAssayResultsViewModel>();

            foreach (Assay a in assayList)
            {
                if (a.StatusID == 3)
                {
                    scheduledAssays.Add(a);
                }
            }

            foreach (Assay a in scheduledAssays)
            {
                LabTechSubmitAssayResultsViewModel viewModel = new LabTechSubmitAssayResultsViewModel();
                viewModel.AssayID = a.AssayID;
                viewModel.LTNumber = a.LTNumber;
                viewModel.CompoundName = db.Compounds.Find(a.LTNumber).CompoundName;
                viewModel.DueDate = db.WorkOrders.Find(a.WorkOrderID).DateDue;
                viewModelList.Add(viewModel);
            }
            return View(viewModelList);
        }

        public ActionResult SubmitSampleResultsIndex(int? AssayID)
        {
            ViewBag.AssayID = AssayID;
            // get all samples for this assayID
            List<SampleTest> sampleTestList = new List<SampleTest>();
            sampleTestList = db.SampleTests.ToList();

            List<SampleTest> samplesForAssay = new List<SampleTest>();
            
            foreach (SampleTest st in sampleTestList)
            {
                if (st.AssayID == AssayID)
                {
                    samplesForAssay.Add(st);
                }
            }

            List<LabTechSubmitSampleResultsViewModel> viewModelList = new List<LabTechSubmitSampleResultsViewModel>();

            foreach (SampleTest st in samplesForAssay)
            {
                LabTechSubmitSampleResultsViewModel viewModel = new LabTechSubmitSampleResultsViewModel();
                viewModel.SampleTestID = st.SampleTestID;
                viewModel.AssayID = st.AssayID;
                viewModel.CompoundName = db.Compounds.Find(st.LTNumber).CompoundName;
                viewModel.TestTubeNumber = (st.LTNumber.ToString() + "-" + st.CompoundSeqCode.ToString());
                if (st.QualResultsPath == null)
                {
                    viewModel.HasQual = false;
                }
                else
                {
                    viewModel.HasQual = true;
                }
                if (st.QuantResultsPath == null)
                {
                    viewModel.HasQuant = false;
                }
                else
                {
                    viewModel.HasQuant = true;
                }
                viewModelList.Add(viewModel);
            }
            return View(viewModelList);
        }

        [Authorize(Roles = "SysAdmin, LabTech")]
        public ActionResult SendToReporting(int? AssayID)
        {
            ViewBag.AssayID = AssayID;
            Assay assay = db.Assays.Find(AssayID);
            assay.StatusID = 5;
            assay.NeedsReports = true;
            db.Entry(assay).State = EntityState.Modified;
            db.SaveChanges();
            return View();
        } 
        
        [Authorize(Roles = "SysAdmin, LabTech")]
        public ActionResult UploadResults(int? SampleTestID, String reportType)
        {
            SampleTest st = db.SampleTests.Find(SampleTestID);
            ViewBag.SampleTestID = SampleTestID;
            ViewBag.TestTubeNumber = (st.LTNumber.ToString() + "-" + st.CompoundSeqCode.ToString());
            ViewBag.AssayID = st.AssayID.ToString();
            if (reportType == "Qual")
            {
                ViewBag.Type = "Qualitative";
            }
            else if (reportType == "Quant")
            {
                ViewBag.Type = "Quantitative";
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SysAdmin, LabTech")]
        public ActionResult UploadResults(HttpPostedFileBase UploadedFile, FormCollection form)
        {
            // parse incoming form
            int SampleTestID = Int32.Parse(form["SampleTestID"]);
            String TestTubeNumber = form["TestTubeNumber"];
            String AssayID = form["AssayID"];
            if (UploadedFile != null)
            {
                if (UploadedFile.ContentLength > 0)
                {
                    if (Path.GetExtension(UploadedFile.FileName) == ".txt")
                    {
                        string fileName = TestTubeNumber + "-" + "AssayNo_" + AssayID + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + ".txt";
                        string folderPath = Path.Combine(Server.MapPath("~/UploadedFiles/txt"), fileName);

                        // save path to server
                        String reportType = form["ReportType"];
                        SampleTest st = db.SampleTests.Find(SampleTestID);
                        if (reportType == "Qual")
                        {
                            st.QualResultsPath = fileName;
                        }
                        else if (reportType == "Quant")
                        {
                            st.QuantResultsPath = fileName;
                        }
                        db.Entry(st).State = EntityState.Modified;
                        db.SaveChanges();
                        UploadedFile.SaveAs(folderPath);
                        ViewBag.Message = "File Uploaded Successfully.";
                        // done saving to folder
                        return RedirectToAction("FileUploadSuccess", "Assays", new { AssayID = st.AssayID});
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
            return View();
        }

        [Authorize(Roles = "SysAdmin, LabTech")]
        public ActionResult FileUploadSuccess(int? AssayID)
        {
            ViewBag.AssayID = AssayID;
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
