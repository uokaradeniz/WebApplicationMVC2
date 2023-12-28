using System;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WebApplicationMVC2.Models;
using System.Web.UI;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WebApplicationMVC2.Controllers
{
    public class HomeController : Controller
    {
        RecipientModel context;

        public HomeController()
        {
            context = new RecipientModel();
        }

        public ActionResult Index()
        {
            //verileri tablede gösterme
            var loginRecipients = context.Recipient.ToList();
            var paymentRecipients = context.RecipientPayment.ToList();

            ViewBag.LoginRecipients = loginRecipients;
            ViewBag.PaymentRecipients = paymentRecipients;


            //databasedeki verilerin yüzdelik dilimi
            var loginRecipientCount = context.Recipient.Count();
            var paymentRecipientCount = context.RecipientPayment.Count();

            double totalCount = loginRecipientCount + paymentRecipientCount;

            double loginRecipientPercentage = (loginRecipientCount / totalCount) * 100;
            double paymentRecipientPercentage = (paymentRecipientCount / totalCount) * 100;

            ViewBag.LoginPercentage = loginRecipientPercentage;
            ViewBag.PaymentPercentage = paymentRecipientPercentage;

            var sentMailData = context.SentMailData.OrderBy(p => p.ID).FirstOrDefault();

            ViewBag.TotalMailCount = sentMailData.TotalEmailsSent;

            return View();
        }

        public async Task<ActionResult> IncrementSentMailCount()
        {
            // İlk kaydı al
            var sentMailData = await context.SentMailData.OrderBy(p => p.ID).FirstOrDefaultAsync();

            if (sentMailData!= null)
            {
                // Değeri 1 arttır
                sentMailData.TotalEmailsSent++;

                // Değişiklikleri kaydet
                await context.SaveChangesAsync();
            }


            return RedirectToAction("Index"); // İsteğe bağlı olarak başka bir sayfaya yönlendirilebilir.
        }

        public ActionResult SendMail()
        {
            DropDownViewModel model = new DropDownViewModel
            {
                templates = GetDropdownItems(),
                SelectedItemId = 1
            };

            return View(model);
        }

        List<SelectListItem> GetDropdownItems()
        {
            var templates = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Amazon sipariş bilgileriniz teyit edilemedi. Lütfen giriş yapıp bilgilerinizi güncelleyin: LİNK", Text = "Amazon Login" },
                    new SelectListItem { Value = "Amazon Prime ayrıcalığı şu anda sadece 1,00 TL! Bu sınırlı süreli kampanyayı kaçırmamak" +
                    " için linkten ödeme bilgilerinizi doğrulayın -Jeff Bezos: LİNK", Text = "Amazon Payment" },
                    new SelectListItem { Value = "Bir kullanıcı sizi X'te dürttü! Cevap vermek için: LİNK", Text = "Twitter Login" },
                    new SelectListItem { Value = "Dikkat! birisi size ait olan instagram hesabına erişmeye çalışıyor olabilir." +
                    " lütfen güvenliğiniz için giriş yapın ve şifrenizi güncelleyin.: LİNK ", Text = "Instagram Login" }
                };
            return templates;
        }

        [HttpPost]
        public async Task<ActionResult> SendMail(string to, string subject, string message, HttpPostedFileBase attachment)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("uguroguzhan1398@gmail.com");
                    mail.To.Add(to);
                    mail.Subject = subject;
                    mail.Body = message;
                    mail.IsBodyHtml = true;

                    if (attachment != null && attachment.ContentLength > 0)
                    {
                        string fileName = System.IO.Path.GetFileName(attachment.FileName);
                        mail.Attachments.Add(new Attachment(attachment.InputStream, fileName));
                    }

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.Credentials = new NetworkCredential("uguroguzhan1398@gmail.com", "uavu lfvx hchx ijdk");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                        await IncrementSentMailCount();
                    }
                }

                ViewBag.Message = "Successfully sent mail!";
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error: {ex.Message}";
            }
            DropDownViewModel model = new DropDownViewModel
            {
                templates = GetDropdownItems(),
                SelectedItemId = 1
            };

            return View(model);
        }
    }
}