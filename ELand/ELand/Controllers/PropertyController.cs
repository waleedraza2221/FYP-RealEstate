using ELand.Models;
using ELand.Models.PropertySteps;
using ELand.Models.ViewModel;
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
                var data = (Step1)Session["step1"];
                return PartialView((Step1)Session["step1"]);

            }
            Session["step1"] = null;
            Session["step2"] = null;
            return View(new Step1());
        }

        //public ActionResult Step2() {

        //    return View();
        //}
        public ActionResult Step2(Step1 model) {

            if (Session["step1"] == null)
            {
                Session["step1"] = new Step1()
                {
                    Address = model.Address,
                    Description = model.Description,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    Title = model.Title
                };
            }

            ViewBag.PurposeID = Purpose();
            ViewBag.TypeID = PTypee();
            ViewBag.AreaID = AreaUnt();
            if (Request.IsAjaxRequest()){
                Step2 data = new Step2();
                if (Session["step2"] != null)
                {
                    data = (Step2)Session["step2"];
                    ViewBag.PurposeID = Purpose(data.PurposeID);
                    ViewBag.TypeID = PTypee(data.TypeID);
                    ViewBag.AreaID = AreaUnt(data.AreaID);
                }
                return PartialView(data);
            }

            return View(new Step2());
        
        }
        public ActionResult Step3(Step2 model)
        {

            var data = (Step1)Session["step1"];

            Session["step2"] = new Step2() {
                ADSL_Cable = model.ADSL_Cable,
                Air_Conditioning = model.Air_Conditioning,
                Area = model.Area,
                AreaID = model.AreaID,
                Bath = model.Bath,
                Bed = model.Bed,
                Digital_Antenna = model.Digital_Antenna,
                Exotic_Garden = model.Exotic_Garden,
                Fridge = model.Fridge,
                Full_HD_TV = model.Full_HD_TV,
                Garage = model.Garage,
                Grill = model.Grill,
                Guest_House = model.Guest_House,
                HiFi_Audio = model.HiFi_Audio,
                Kitchen = model.Kitchen,
                Kitchen_With_Island = model.Kitchen_With_Island,
                Pool = model.Pool,
                Price = model.Price,
                PurposeID = model.PurposeID,
                TypeID = model.TypeID,
                WiFi = model.WiFi
            };

            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            
            return View();
        }

        public ActionResult Step4(Step3 model) {
            Step1 stp1 = (Step1)Session["step1"];
            Step2 stp2 = (Step2)Session["step2"];
            PropertyViewModel propertyVM = new PropertyViewModel() {
                Address = stp1.Address,
                Title = stp1.Title,
                Description = stp1.Description,
                Latitude = stp1.Latitude,
                Longitude = stp1.Longitude,
                ADSL_Cable = stp2.ADSL_Cable,
                Air_Conditioning = stp2.Air_Conditioning,
                Area = stp2.Area,
                AreaID = stp2.AreaID,
                Bath = stp2.Bath,
                Bed = stp2.Bed,
                Digital_Antenna = stp2.Digital_Antenna,
                Exotic_Garden = stp2.Exotic_Garden,
                Fridge = stp2.Fridge,
                Full_HD_TV = stp2.Full_HD_TV,
                Garage = stp2.Garage,
                Grill = stp2.Grill,
                Guest_House = stp2.Guest_House,
                HiFi_Audio = stp2.HiFi_Audio,
                Kitchen = stp2.Kitchen,
                Kitchen_With_Island = stp2.Kitchen_With_Island,
                Pool = stp2.Pool,
                Price = stp2.Price,
                PurposeID = stp2.PurposeID,
                TypeID = stp2.TypeID,
                WiFi = stp2.WiFi
            };
            string Mainimg = "";
            if (model.MainImage != null)
            {
                Guid g;
                g = Guid.NewGuid();
                Mainimg = System.IO.Path.GetFileName(model.MainImage.FileName);
                Mainimg = g + Mainimg;
                model.MainImage.SaveAs(Server.MapPath("~/Images/Property/" + Mainimg));
            }
            else
            {
                Mainimg = "PropertyMainImage.jpg";
            }
            if (model.GalleryImages != null)
            {
                List<string> imgs = new List<string>();
                foreach (var item in model.GalleryImages)
                {
                    string s = "";
                    Guid g;
                    g = Guid.NewGuid();
                    s = System.IO.Path.GetFileName(item.FileName);
                    s = g +s;
                    item.SaveAs(Server.MapPath("~/Images/Property/" + s));
                    imgs.Add(s);
                }
                propertyVM.GalleryImages = imgs;
            }
            propertyVM.MainImage = Mainimg;

            return View(propertyVM);
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