﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ELand.Models;
using System.Net.Mail;
using ELand.Helper;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace ELand.Areas.Admin.Controllers
{
   [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public Task SendAsync(string touser, string body)
        {
            var result = Task.FromResult(0);
            try
            {

                MailMessage message = new MailMessage();
                message.From = new MailAddress(WebConfigurationManager.AppSettings["userid"]);
                message.To.Add(new MailAddress(touser));
                message.Subject = "No Reply";
                message.Body = body;
                message.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(WebConfigurationManager.AppSettings["smtp"]);
                smtp.Credentials = new System.Net.NetworkCredential(WebConfigurationManager.AppSettings["userid"], WebConfigurationManager.AppSettings["pass"]);
                smtp.Port = 25;
                smtp.EnableSsl = false;
                smtp.Send(message);



                result = Task.FromResult(1);
            }
            catch (Exception e)
            {
                result = Task.FromResult(0);
            }


            return result;


        }


        public ActionResult IsSeen(int Id) {

            var item =db.Contact.FirstOrDefault(x=>x.Id==Id);
            item.IsRead = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return View(item);
        }


        [HttpPost]
        public ActionResult IsSeen(string rmessage,string Email) {
            // Send Email Or Phone Message Here
          //  Utility.SendAsync("admin@admin.com","waleedraza221@gmail.com","Reply",rmessage);
            SendAsync(Email, rmessage);
            return RedirectToAction("Index","Contact");
        }

        public ActionResult Contact_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Contact> contact = db.Contact;
            DataSourceResult result = contact.ToDataSourceResult(request, x => new {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Message = x.Message,
                IsRead = x.IsRead
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Contact_Destroy([DataSourceRequest]DataSourceRequest request, Contact contact)
        {
            if (ModelState.IsValid)
            {
                var entity = new Contact
                {
                    Id = contact.Id,
                    Name = contact.Name,
                    Email = contact.Email,
                    Phone = contact.Phone,
                    Message = contact.Message,
                    IsRead = contact.IsRead
                };

                db.Contact.Attach(entity);
                db.Contact.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { contact }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
