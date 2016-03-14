using ELand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.Data.Entity;
namespace ELand.Controllers
{
    public class AgencyController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Agency
        public ActionResult Index(int Id,int? page)
        {
            ViewBag.Agency = db.Agency.FirstOrDefault(x => x.Id == Id);
            ViewBag.AgencyAdmin = db.Users.FirstOrDefault(x => x.IsAgencyAdmin == true && x.AgencyId == Id);
            ViewBag.Agents = db.Users.Where(x => x.AgencyId == Id).ToList();
           
            List<Property> p = new List<Property>();
            foreach (var agent in db.Users.Where(x => x.AgencyId == Id).ToList()) {

                foreach (var item in db.Property.Where(x => x.UserId == agent.Id).ToList())
                {
                    p.Add(item);

                }
            
            
            }

            ViewBag.Total = p.Count();


            return View(p.OrderByDescending(x=>x.Id).ToPagedList(page ?? 1,8));
        }


        public ActionResult Properties(int Id, int? page)
        {
            List<Property> p = new List<Property>();
            foreach (var agent in db.Users.Where(x => x.AgencyId == Id).ToList())
            {

                foreach (var item in db.Property.Where(x => x.UserId == agent.Id).ToList())
                {
                    p.Add(item);

                }


            }
            return View(p.OrderByDescending(x=>x.Id).ToPagedList(page ?? 1, 12));

        }


        public ActionResult List(int? page)
        {


            return View(db.Agency.OrderByDescending(x => x.Id).ToPagedList(page ?? 1, 12));
        }


        [Authorize]
        public ActionResult Edit(int Id)
        {
            var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if (user.AgencyId==Id&&user.IsAgencyAdmin==true)
            {
                return View(db.Agency.FirstOrDefault(x => x.Id == Id));
            }
            return RedirectToAction("Error403", "Home"); 
            }
        [HttpPost]
        [Authorize]
        public ActionResult Edit(Agency model,HttpPostedFileBase img){

            string s = "";

            if(ModelState.IsValid){

                if (img != null)
                {
                    Guid g = Guid.NewGuid();
                    s = g + img.FileName;
                    img.SaveAs(Server.MapPath("~/Images/Agency/" + s));
                    model.Image = s;
                }
                

             db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Dashboard");
            }
            
            return View(model);
        }

    }
}