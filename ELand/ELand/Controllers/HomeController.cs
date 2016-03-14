using ELand.Helper;
using ELand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace ELand.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Error403() {

            return View();
        }
        public ActionResult Error404()
        {

            return View();
        }
        ApplicationDbContext db = new ApplicationDbContext();
        public JsonResult IsExist(string Title)
        {
            return Json(!db.Agency.Any(x => x.Title == Title),JsonRequestBehavior.AllowGet);
        }

        public IEnumerable<SelectListItem> Purpose(int id = 0)
        {

            List<SelectListItem> it = new List<SelectListItem>();
            var lst = db.Purpose.ToList();
            foreach (var item in lst)
            {
                SelectListItem i = new SelectListItem()
                {
                    Text = item.Title,
                    Value = Convert.ToString(item.Id),
                    Selected = item.Id == id ? true : false
                };
                it.Add(i);

            }

            return it;
        }
        public IEnumerable<SelectListItem> PTypee(int id = 0)
        {

            List<SelectListItem> it = new List<SelectListItem>();
            var lst = db.PType.ToList();
            foreach (var item in lst)
            {
                SelectListItem i = new SelectListItem()
                {
                    Text = item.Title,
                    Value = Convert.ToString(item.Id),
                    Selected = item.Id == id ? true : false
                };
                it.Add(i);

            }

            return it;
        }

        public IEnumerable<SelectListItem> AreaUnt(int id = 0)
        {

            List<SelectListItem> it = new List<SelectListItem>();
            var lst = db.AreaUnit.ToList();
            foreach (var item in lst)
            {
                SelectListItem i = new SelectListItem()
                {
                    Text = item.Title,
                    Value = Convert.ToString(item.Id),
                    Selected = item.Id == id ? true : false
                };
                it.Add(i);

            }

            return it;
        }

        public IEnumerable<SelectListItem> CityID(int id = 0)
        {

            List<SelectListItem> it = new List<SelectListItem>();
            var lst = db.Cities.OrderBy(x => x.Name).ToList();
            foreach (var item in lst)
            {
                SelectListItem i = new SelectListItem()
                {
                    Text = item.Name,
                    Value = Convert.ToString(item.Id),
                    Selected = item.Id == id ? true : false
                };
                it.Add(i);

            }

            return it;
        }

     

      public ActionResult Index()
        {




            @ViewBag.title = "A Real Estate Website";
            ViewBag.CityId = CityID();
            ViewBag.PropertyStatus = Purpose();
            ViewBag.PropertyType = PTypee();
            ViewBag.Properties = db.Property.Take(6).ToList();
            ViewBag.LatestProperties = db.Property.OrderByDescending(x=>x.Id).Take(6).ToList();


            return View();
        }

      public ActionResult Latest() {

          return PartialView(db.Property.OrderByDescending(X => X.Id).Take(3).ToList());
      }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            
            return View();
        }


        public ActionResult Faq()
        {
           

            return View(db.Faq.ToList());
        }

        public ActionResult Terms()
        {


            return View();
        }

        public ActionResult Policy()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact model)
        {
            model.IsRead = false;
            db.Contact.Add(model);
            db.SaveChanges();
            return Content("Thanks For Contacting We will get You Soon!");
        }
    }
}