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
    public class AreaUnitController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AreaUnit_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<AreaUnit> areaunit = db.AreaUnit;
            DataSourceResult result = areaunit.ToDataSourceResult(request, areaUnit => new {
                Id = areaUnit.Id,
                Title = areaUnit.Title
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AreaUnit_Create([DataSourceRequest]DataSourceRequest request, AreaUnit areaUnit)
        {
            if (ModelState.IsValid)
            {
                var entity = new AreaUnit
                {
                    Title = areaUnit.Title
                };

                db.AreaUnit.Add(entity);
                db.SaveChanges();
                areaUnit.Id = entity.Id;
            }

            return Json(new[] { areaUnit }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AreaUnit_Update([DataSourceRequest]DataSourceRequest request, AreaUnit areaUnit)
        {
            if (ModelState.IsValid)
            {
                var entity = new AreaUnit
                {
                    Id = areaUnit.Id,
                    Title = areaUnit.Title
                };

                db.AreaUnit.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { areaUnit }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AreaUnit_Destroy([DataSourceRequest]DataSourceRequest request, AreaUnit areaUnit)
        {
            if (ModelState.IsValid)
            {
                var entity = new AreaUnit
                {
                    Id = areaUnit.Id,
                    Title = areaUnit.Title
                };

                db.AreaUnit.Attach(entity);
                db.AreaUnit.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { areaUnit }.ToDataSourceResult(request, ModelState));
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
