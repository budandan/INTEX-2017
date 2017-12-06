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
    public class CallTicketsController : Controller
    {
        private IntexContext db = new IntexContext();

        // GET: CallTickets
        public ActionResult Index()
        {
            return View(db.CallTickets.ToList());
        }

        // GET: CallTickets/Details/5
        public ActionResult Details(int? id)
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

        // GET: CallTickets/Create
        public ActionResult Create()
        {
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
                db.CallTickets.Add(callTicket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(callTicket);
        }

        // GET: CallTickets/Edit/5
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
