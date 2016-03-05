using ELand.Models;
using ELand.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELand.Controllers
{    
    [Authorize]
    public class PropertyController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Property
        public ActionResult Index(int Id)
        {
            var data=db.Property.FirstOrDefault(x=>x.Id==Id);
            ViewBag.Contact = db.Users.FirstOrDefault(x => x.Id == data.UserId);
            return View(data);
        }

        public ActionResult List(SearchProperty search) {

            ViewBag.CityId = CityID();
            ViewBag.PropertyStatus = Purpose();
            ViewBag.PropertyType = PTypee();

            return View(db.Property.ToList());
        }

        public ActionResult Feature() {

            return PartialView(db.Property.OrderBy(x=>x.Area).Take(5).ToList());
        }
        public ActionResult Recent() {

            return PartialView(db.Property.OrderByDescending(x => x.Id).Take(3).ToList());
        }
        public ActionResult Create()
        {
            ViewBag.AreaID = AreaUnt();
            ViewBag.TypeID = PTypee();
            ViewBag.PurposeID = Purpose();
            ViewBag.CityId = CityID();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Property model, HttpPostedFileBase titleimg, HttpPostedFileBase[] gimages)
        {


            if (ModelState.IsValid) {
                Guid g = Guid.NewGuid();

                string title = "";
                string gimg = "";
                if (titleimg != null) {
                    Directory.CreateDirectory(Server.MapPath("~/Images/Property/" + g));
                    if (Directory.Exists(Server.MapPath("~/Images/Property/" + g))) {
                        title = g+titleimg.FileName;
                        titleimg.SaveAs(Server.MapPath("~/Images/Property/" + g+"/"+title));
                 }
                }
                if (gimages != null) {
                    if (Directory.Exists(Server.MapPath("~/Images/Property/" + g))==false)
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Images/Property/" + g));
                    }
                    if (Directory.Exists(Server.MapPath("~/Images/Property/" + g))) {

                        foreach (var img in gimages)
                        {
                            gimg+=img.FileName;
                            img.SaveAs(Server.MapPath("~/Images/Property/"+g+"/"+img.FileName));
                            gimg += ",";
                        }
                    }
                }

                gimg = gimg.Remove(gimg.Length - 1, 1);
                model.GlobalId = g.ToString();
                model.MainImage = title;
                model.GalleryImages = gimg;
                model.UserId = db.Users.FirstOrDefault(x=>x.Email==User.Identity.Name).Id;
                model.PublishDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                db.Property.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Dashboard");
            }



            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var data = db.Property.FirstOrDefault(x => x.Id == id);
            ViewBag.AreaID = AreaUnt(data.AreaID);
            ViewBag.TypeID = PTypee(data.TypeID);
            ViewBag.PurposeID = Purpose(data.PurposeID);
            ViewBag.CityId = CityID(data.CityId);

            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Property model, HttpPostedFileBase titleimg, HttpPostedFileBase[] gimages)
        {
            if (ModelState.IsValid)
            {
                string g = model.GlobalId;

                string title = "";
                string gimg = "";
                if (titleimg != null)
                {
                    if (model.MainImage != "noimage.png")
                    {
                        System.IO.File.Delete(Server.MapPath("~/Images/Property/" + g+"/"+model.MainImage));
                    }
                    if (Directory.Exists(Server.MapPath("~/Images/Property/" + g)))
                    {
                        title = g + titleimg.FileName;
                        titleimg.SaveAs(Server.MapPath("~/Images/Property/" + g + "/" + title));
                        model.MainImage = title;
                    }
                }
                if (gimages != null)
                {
                    
                    if (Directory.Exists(Server.MapPath("~/Images/Property/" + g)))
                    {
                        string[] s = model.GalleryImages.Split(',');
                        foreach (var i in s)
                        {
                            System.IO.File.Delete(Server.MapPath("~/Images/Property/" + g + "/" + i));
                        }

                        foreach (var img in gimages)
                        {
                            gimg += img.FileName;
                            img.SaveAs(Server.MapPath("~/Images/Property/" + g + "/" + img.FileName));
                            gimg += ",";
                        }
                    }
                    gimg = gimg.Remove(gimg.Length - 1, 1);
                    model.GalleryImages = gimg;
                }
                model.UpdateDate = DateTime.Now;

          db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
               
                return RedirectToAction("Index", "Dashboard");
            }



            return View(model);
          
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

        public IEnumerable<SelectListItem> CityID(int id = 0)
        {

            List<SelectListItem> it = new List<SelectListItem>();
            var lst = db.Cities.OrderBy(x=>x.Name).ToList();
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

    
   
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    }

    




    }
