using ELand.Models;
using ELand.Models.ViewModels;
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
    [Authorize(Roles = "Admin")]

    public class PropertyController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Property
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Property_Read([DataSourceRequest]DataSourceRequest request)
        {

            List<ViewProperty> Property = GetProperties();
            DataSourceResult result = Property.ToDataSourceResult(request, x => new
            {
                Id = x.Id,
                Title = x.Title,
                MainImage = x.MainImage,
                GlobalId = x.GlobalId,
                City = x.City


            });
            return Json(result);
        }

        private List<ViewProperty> GetProperties()
        {
          
            var data = db.Property.ToList();
            List<ViewProperty> d = new List<ViewProperty>();
            foreach (var item in data)
            {
                ViewProperty p = new ViewProperty()
                {
                    City = db.Cities.FirstOrDefault(x => x.Id == item.CityId).Name,
                    Description = item.Description,
                    GlobalId = item.GlobalId,
                    MainImage = item.MainImage,
                    Price = item.Price,
                    Title = item.Title,
                    Id = item.Id
                };
                d.Add(p);

            }
            return d;
        }



        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Property_Create([DataSourceRequest]DataSourceRequest request, ViewProperty Property)
        {
            if (ModelState.IsValid)
            {
                var entity = new Property
                {
                    Title = Property.Title
                };

                db.Property.Add(entity);
                db.SaveChanges();
                Property.Id = entity.Id;
            }

            return Json(new[] { Property }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Property_Update([DataSourceRequest]DataSourceRequest request, ViewProperty Property)
        {
            if (ModelState.IsValid)
            {
                var entity = new Property
                {
                    Id = Property.Id,
                    Title = Property.Title
                };

                db.Property.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { Property }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Property_Destroy([DataSourceRequest]DataSourceRequest request, ViewProperty Property)
        {

            var entity = db.Property.FirstOrDefault(x => x.Id == Property.Id);

            db.Property.Attach(entity);
            db.Property.Remove(entity);
            db.SaveChanges();


            return Json(new[] { Property }.ToDataSourceResult(request, ModelState));
        }



    }
}