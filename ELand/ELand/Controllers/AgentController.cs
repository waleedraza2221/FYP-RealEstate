using ELand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ELand.Controllers
{
    public class AgentController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Agent
        public ActionResult Index(string id, int? page)
        {
            ViewBag.agent = db.Users.FirstOrDefault(x => x.Id == id);
            return View(db.Property.Where(x => x.UserId == id).OrderByDescending(x=>x.Id).ToPagedList(page ?? 1, 8));
        }
        public ActionResult Properties(string Id, int? page)
        {


            return View(db.Property.Where(x => x.UserId == Id).OrderByDescending(x=>x.Id).ToPagedList(page ?? 1, 12));
        }
       [Authorize]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create(IndividualViewModel model, HttpPostedFileBase pimage)
        {
            if (ModelState.IsValid)
            {
                string img = "";
                if (pimage != null)
                {
                    Guid g;
                    g = Guid.NewGuid();
                    img = System.IO.Path.GetFileName(pimage.FileName);
                    img = g + img;
                    /*Saving the file in server folder*/
                    pimage.SaveAs(Server.MapPath("~/Images/Profile/" + img));
                }
                else
                {
                    img = "ProfileImage.jpg";
                }
                var admin = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                var userStore = new UserStore<ApplicationUser>(db);
                var UserManager = new UserManager<ApplicationUser>(userStore);
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Phone = model.Phone, Skype = model.Skype, Twitter = model.Twitter, Instagram = model.Instagram, FirstName = model.FirstName, LastName = model.LastName, PhoneNumber = model.Phone, ProfileImage = img, AgencyId=admin.AgencyId,IsAgencyAdmin=false };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    return RedirectToAction("Index", "Dashboard");
                }
                AddErrors(result);
            }
            return View(model);
        }



        public ActionResult List(int? page)
        {



            return View(db.Users.OrderBy(x => x.FirstName).ToPagedList(page ?? 1, 12));
        }



        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


    }
}