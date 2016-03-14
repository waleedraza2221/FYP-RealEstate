using ELand.Models;
using ELand.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace ELand.Controllers
{    
   
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

        public ActionResult List(SearchProperty search,int? page)
        {
            int bath=Convert.ToInt32(search.BathRoom);
            int bed = Convert.ToInt32(search.BedRoom);
            Int64 sprice = 0;
            Int64 eprice = 0;
            if (search.Price != null) {
                string[] p = search.Price.Split(',');
                sprice = Convert.ToInt64(p[0]);
                eprice = Convert.ToInt64(p[1]);
            }
            //string[] price = search.Price.Split('-');
            var data = (from p in db.Property where
                            (search.Id ==0 ? true:p.Id==search.Id)
                            && 
                            (search.CityId==0 ? true:p.CityId==search.CityId)
                            &&
                            (search.PropertyStatus==0? true:p.PurposeID==search.PropertyStatus)
                            &&
                            (search.PropertyType==0? true:p.TypeID==search.PropertyType)
                            &&
                            (bath==0? true:bath<=2 ? p.Bath==bath:p.Bath>2)
                             &&
                            (bed == 0 ? true : bed <= 2 ? p.Bed == bed : p.Bed > 2)
                           &&
                           (sprice==0? true:p.Price>=sprice&&p.Price<=eprice)
                        orderby  p.Id select p);
            //(from t in TheDC.SomeTable

            // where TheIDs.Contains(t.ID) && (
            // t.column1.Contains(TheSearchTerm) ||
            // t.column2.Contains(TheSearchTerm) ||
            // t.column3.Contains(TheSearchTerm))
            // select t.ID).ToList();
            var d = data.ToList();


            ViewBag.CityId = CityID(search.CityId);
            ViewBag.PropertyStatus = Purpose(search.PropertyStatus);
            ViewBag.PropertyType = PTypee(search.PropertyType);
            ViewBag.Total = d.Count();
           
            return View(data.ToPagedList(page ?? 1,12));
        }


        public ActionResult Map()
        {
            return View(db.Property.ToList());
        }


        public ActionResult Feature() {

            return PartialView(db.Property.OrderBy(x=>x.Area).Take(5).ToList());
        }
        public ActionResult Recent() {

            return PartialView(db.Property.OrderByDescending(x => x.Id).Take(3).ToList());
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.AreaID = AreaUnt();
            ViewBag.TypeID = PTypee();
            ViewBag.PurposeID = Purpose();
            ViewBag.CityId = CityID();
            return View();
        }
        [HttpPost]
        [Authorize]
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
        [Authorize]
        public ActionResult Edit(int id)
        {
           var data = db.Property.FirstOrDefault(x => x.Id == id);
            var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if (data.UserId != user.Id)
            {
                return RedirectToAction("Error403", "Home");
            }



            ViewBag.AreaID = AreaUnt(data.AreaID);
            ViewBag.TypeID = PTypee(data.TypeID);
            ViewBag.PurposeID = Purpose(data.PurposeID);
            ViewBag.CityId = CityID(data.CityId);

            return View(data);
        }
        [HttpPost]
        [Authorize]
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
               int a= gimages.Count();
                if (gimages[0] != null)
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
