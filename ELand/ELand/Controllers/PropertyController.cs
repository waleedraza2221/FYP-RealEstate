using ELand.Models;
using ELand.Models.PropertySteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELand.Controllers
{
    public class PropertyController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Property
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add_Property()
        {
            if (Request.IsAjaxRequest())
            {
               
                return PartialView((Step1)Session["step1"]);
            }
            return View();
        }

        //public ActionResult Step2() {

        //    return View();
        //}
        public ActionResult Step2(Step1 model) {

            Session["step1"] = new Step1() { 
            Address=model.Address,
            Description=model.Description,
            Latitude=model.Latitude,
            Longitude=model.Longitude,
            Title=model.Title
            };
            if (Request.IsAjaxRequest()) {
                return PartialView();
            }

            return View();
        }
        public ActionResult Step3(Step2 model)
        {
            ViewBag.PurposeID = Purpose();
            ViewBag.TypeID = PTypee();
            ViewBag.AreaID = AreaUnt();
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            
            return View(new Step3());
        }

        public ActionResult Step4(Step3 model) {

            return View();
        }

        public IEnumerable<SelectListItem> Purpose(int id=0) {

            List<SelectListItem> it = new List<SelectListItem>();
            var lst = db.Purpose.ToList();
            foreach (var item in lst)
            {
                SelectListItem i = new SelectListItem()
                {
                    Text = item.Title,
                    Value = Convert.ToString(item.Id),
                    Selected = item.Id==id? true:false
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

    }
}