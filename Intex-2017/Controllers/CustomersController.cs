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

namespace Intex_2017.Controllers
{
    public class CustomersController : Controller
    {
        private IntexContext db = new IntexContext();

        // GET: Customers
        [Authorize(Roles = "SysAdmin, SalesAgent")]
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        [Authorize(Roles = "SysAdmin, SalesAgent, Customer")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
			List<PaymentMethod> customer_Payments = new List<PaymentMethod>();
			customer_Payments = db.PaymentMethods.ToList();
			ViewBag.MyList = customer_Payments.ToList();
            return View();
        }


		// POST: Customers/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustID,CustFirstName,CustLastName,CustAddress1,CustAddress2,CustCity,CustState,CustZip,CustEmail,CustPhone,PaymentMethodID,CustUserName,CustPassword")] Customer customer)
        {
			List<Customer> checkList = new List<Customer>();
			checkList = db.Customers.ToList();
			for (int i = 0; i < checkList.Count; i++)
			{
				if (customer.CustUserName == checkList[i].CustUserName)
				{
					List<PaymentMethod> customer_Payments = new List<PaymentMethod>();
					customer_Payments = db.PaymentMethods.ToList();
					ViewBag.MyList = customer_Payments.ToList();
					return View(customer);
				}
			}
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize(Roles = "SysAdmin, SalesAgent, Customer")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustID,CustFirstName,CustLastName,CustAddress1,CustAddress2,CustCity,CustState,CustZip,CustEmail,CustPhone,PaymentMethodID,CustPassword")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        [Authorize(Roles = "SysAdmin, SalesAgent")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
