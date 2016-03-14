using ELand.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Kendo.Mvc.Extensions;
using System.Web.Mvc;

namespace ELand.Areas.Admin.Controllers
{
    [Authorize(Roles="Admin")]
    public class AgencyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Agency_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Agency> agency = db.Agency;
            DataSourceResult result = agency.ToDataSourceResult(request, x => new {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Skype = x.Skype,
                Facebook = x.Facebook,
                Twitter = x.Twitter,
                Instagram = x.Instagram,
                Email = x.Email,
                Mobile = x.Mobile,
                Image = x.Image
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Agency_Destroy([DataSourceRequest]DataSourceRequest request, Agency agency)
        {
            if (ModelState.IsValid)
            {
                var entity = new Agency
                {
                    Id = agency.Id,
                    Title = agency.Title,
                    Description = agency.Description,
                    Skype = agency.Skype,
                    Facebook = agency.Facebook,
                    Twitter = agency.Twitter,
                    Instagram = agency.Instagram,
                    Email = agency.Email,
                    Mobile = agency.Mobile,
                    Image = agency.Image
                };

                db.Agency.Attach(entity);
                db.Agency.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { agency }.ToDataSourceResult(request, ModelState));
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
