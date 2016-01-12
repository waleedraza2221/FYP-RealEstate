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
    public class TypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Type_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<PType> ptype = db.PType;
            DataSourceResult result = ptype.ToDataSourceResult(request, type => new {
                Id = type.Id,
                Title = type.Title
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Type_Create([DataSourceRequest]DataSourceRequest request, PType type)
        {
            if (ModelState.IsValid)
            {
                var entity = new PType
                {
                    Title = type.Title
                };

                db.PType.Add(entity);
                db.SaveChanges();
                type.Id = entity.Id;
            }

            return Json(new[] { type }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Type_Update([DataSourceRequest]DataSourceRequest request, PType type)
        {
            if (ModelState.IsValid)
            {
                var entity = new PType
                {
                    Id = type.Id,
                    Title = type.Title
                };

                db.PType.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { type }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Type_Destroy([DataSourceRequest]DataSourceRequest request, PType type)
        {
            if (ModelState.IsValid)
            {
                var entity = new PType
                {
                    Id = type.Id,
                    Title = type.Title
                };

                db.PType.Attach(entity);
                db.PType.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { type }.ToDataSourceResult(request, ModelState));
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
