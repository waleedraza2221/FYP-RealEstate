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

namespace ELand.Areas.Admin.Controllers
{
    public class ContactController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult IsSeen(int Id) {

            var item =db.Contact.FirstOrDefault(x=>x.Id==Id);
            item.IsRead = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return View(item);
        }


        [HttpPost]
        public ActionResult IsSeen(string rmessage) {
            // Send Email Or Phone Message Here
            Utility.SendAsync("admin@admin.com","waleedraza221@gmail.com","Reply",rmessage);

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
