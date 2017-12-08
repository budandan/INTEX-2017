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
    public class CallTicketsController : Controller
    {
        private IntexContext db = new IntexContext();

        // GET: CallTickets
        [Authorize(Roles = "SalesAgent, SysAdmin")]
        public ActionResult Index()
        {
            List<OpenTicketsViewModel> viewModelList = new List<OpenTicketsViewModel>();
            List<Customer> customerList = new List<Customer>();
            List<CallTicket> callTicketList = new List<CallTicket>();

            customerList = db.Customers.ToList();
            callTicketList = db.CallTickets.ToList();

            foreach (CallTicket ct in callTicketList)
            {
                OpenTicketsViewModel viewModel = new OpenTicketsViewModel();
                Customer c = new Customer();
                c = db.Customers.Find(ct.CustID);
                viewModel.CustFirstName = c.CustFirstName;
                viewModel.CustLastName = c.CustLastName;
                viewModel.CustCompany = c.CustCompany;
                viewModel.Subject = ct.Subject;
                viewModel.CallTicketID = ct.CallTicketID;
                viewModelList.Add(viewModel);
            }

            return View(viewModelList);
        }

        public ActionResult CustomerConfirmTicket()
        {
            return View();
        }

        // GET: CallTickets/Details/5
        [Authorize(Roles = "SalesAgent, SysAdmin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CallTicket ct = db.CallTickets.Find(id);
            OpenTicketsViewModel viewModel = new OpenTicketsViewModel();
            Customer c = new Customer();
            c = db.Customers.Find(ct.CustID);
            viewModel.CustFirstName = c.CustFirstName;
            viewModel.CustLastName = c.CustLastName;
            viewModel.CustCompany = c.CustCompany;
            viewModel.Subject = ct.Subject;
            viewModel.CallTicketID = ct.CallTicketID;
            viewModel.Body = ct.Body;
            ViewBag.CustID = ct.CustID;
            viewModel.CustPhone = c.CustPhone;
            if (ct == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: CallTickets/Create
        [Authorize(Roles = "SalesAgent, SysAdmin, Customer")]
        public ActionResult Create(int? id)
        {
            var subjectOptions = new Dictionary<String, String>
            {
                { "Price Quote", "Price Quote" },
                { "Question", "Question" },
                { "Complaint", "Complaint" },
                { "Other", "Other" },
            };
            ViewBag.subjectOptions = new SelectList(subjectOptions, "Key", "Value");
            ViewBag.custID = id;
            return View();
        }

        // POST: CallTickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CallTicketID,CustID,Subject,Body,IsOpen")] CallTicket callTicket)
        {
            if (ModelState.IsValid)
            {
                callTicket.CustID = Int32.Parse(User.Identity.Name);
                callTicket.IsOpen = true;
                db.CallTickets.Add(callTicket);
                db.SaveChanges();
                if (User.IsInRole("Customer"))
                {
                    return RedirectToAction("CustomerConfirmTicket");
                }
                else if (User.IsInRole("SalesAgent") || User.IsInRole("SysAdmin"))
                {
                    return RedirectToAction("Index");
                }
            }

            var subjectOptions = new Dictionary<String, String>
            {
                { "Price Quote", "Price Quote" },
                { "Question", "Question" },
                { "Complaint", "Complaint" },
                { "Other", "Other" },
            };
            ViewBag.subjectOptions = new SelectList(subjectOptions, "Key", "Value");

            return View(callTicket);
        }

        // GET: CallTickets/Edit/5
        [Authorize(Roles = "SalesAgent, SysAdmin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CallTicket callTicket = db.CallTickets.Find(id);
            if (callTicket == null)
            {
                return HttpNotFound();
            }
            return View(callTicket);
        }

        // POST: CallTickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CallTicketID,CustID,Subject,Body,IsOpen")] CallTicket callTicket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(callTicket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(callTicket);
        }

        // GET: CallTickets/Delete/5
        [Authorize(Roles = "SalesAgent, SysAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CallTicket callTicket = db.CallTickets.Find(id);
            if (callTicket == null)
            {
                return HttpNotFound();
            }
            return View(callTicket);
        }

        // POST: CallTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CallTicket callTicket = db.CallTickets.Find(id);
            db.CallTickets.Remove(callTicket);
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
