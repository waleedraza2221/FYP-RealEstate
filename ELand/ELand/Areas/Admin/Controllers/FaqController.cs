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

namespace ELand.Areas.Admin.Controllers
{
    public class FaqController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Faq_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Faq> faq = db.Faq;
            DataSourceResult result = faq.ToDataSourceResult(request, x => new {
                Id = x.Id,
                Question = x.Question,
                Answer = x.Answer
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Faq_Create([DataSourceRequest]DataSourceRequest request, Faq faq)
        {
            if (ModelState.IsValid)
            {
                var entity = new Faq
                {
                    Question = faq.Question,
                    Answer = faq.Answer
                };

                db.Faq.Add(entity);
                db.SaveChanges();
                faq.Id = entity.Id;
            }

            return Json(new[] { faq }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Faq_Update([DataSourceRequest]DataSourceRequest request, Faq faq)
        {
            if (ModelState.IsValid)
            {
                var entity = new Faq
                {
                    Id = faq.Id,
                    Question = faq.Question,
                    Answer = faq.Answer
                };

                db.Faq.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { faq }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Faq_Destroy([DataSourceRequest]DataSourceRequest request, Faq faq)
        {
            if (ModelState.IsValid)
            {
                var entity = new Faq
                {
                    Id = faq.Id,
                    Question = faq.Question,
                    Answer = faq.Answer
                };

                db.Faq.Attach(entity);
                db.Faq.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { faq }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    
        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
