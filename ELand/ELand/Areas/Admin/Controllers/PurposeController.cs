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
    [Authorize(Roles = "Admin")]

    public class PurposeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Purpose_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Purpose> purpose = db.Purpose;
            DataSourceResult result = purpose.ToDataSourceResult(request, x => new {
                Id = x.Id,
                Title = x.Title
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Purpose_Create([DataSourceRequest]DataSourceRequest request, Purpose purpose)
        {
            if (ModelState.IsValid)
            {
                var entity = new Purpose
                {
                    Title = purpose.Title
                };

                db.Purpose.Add(entity);
                db.SaveChanges();
                purpose.Id = entity.Id;
            }

            return Json(new[] { purpose }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Purpose_Update([DataSourceRequest]DataSourceRequest request, Purpose purpose)
        {
            if (ModelState.IsValid)
            {
                var entity = new Purpose
                {
                    Id = purpose.Id,
                    Title = purpose.Title
                };

                db.Purpose.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { purpose }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Purpose_Destroy([DataSourceRequest]DataSourceRequest request, Purpose purpose)
        {
            if (ModelState.IsValid)
            {
                var entity = new Purpose
                {
                    Id = purpose.Id,
                    Title = purpose.Title
                };

                db.Purpose.Attach(entity);
                db.Purpose.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { purpose }.ToDataSourceResult(request, ModelState));
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
