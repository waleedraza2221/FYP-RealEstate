using ELand.Helper;
using ELand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mail;
using System.Web.Mvc;

namespace ELand.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public JsonResult IsExist(string Title)
        {
            return Json(!db.Agency.Any(x => x.Title == Title),JsonRequestBehavior.AllowGet);
        }




      public ActionResult Index()
        {
            @ViewBag.title = "A Real Estate Website";
         // await Utility.SendAsync("king57007@gmail.com", "waleedraza221@gmail.com", "Hello", "Hellofghdfg");
            return View();
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
    }
}