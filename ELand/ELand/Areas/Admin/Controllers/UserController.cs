﻿using ELand.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ELand.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]

    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest()) {

                return PartialView();
            }
            return View();
        }
        public ActionResult User_Create() {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            return View();
        }
        public ActionResult User_Details(string id) {
            if (Request.IsAjaxRequest())
            {
                return PartialView(db.Users.FirstOrDefault(x => x.Id == id));
            }
            return View(db.Users.FirstOrDefault(x=>x.Id==id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult User_Delete([DataSourceRequest]DataSourceRequest request, ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Remove(db.Users.FirstOrDefault(x=>x.Id==user.Id));
                db.SaveChanges();
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult ApplicationUsers_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<ApplicationUser> applicationusers = db.Users;
            DataSourceResult result = applicationusers.ToDataSourceResult(request, applicationUser => new {
                Id = applicationUser.Id,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Phone = applicationUser.Phone,
                Skype = applicationUser.Skype,
                Twitter = applicationUser.Twitter,
                Instagram = applicationUser.Instagram,
                IsAgencyAdmin = applicationUser.IsAgencyAdmin,
                Email = applicationUser.Email,
                EmailConfirmed = applicationUser.EmailConfirmed,
                PasswordHash = applicationUser.PasswordHash,
                SecurityStamp = applicationUser.SecurityStamp,
                PhoneNumber = applicationUser.PhoneNumber,
                PhoneNumberConfirmed = applicationUser.PhoneNumberConfirmed,
                TwoFactorEnabled = applicationUser.TwoFactorEnabled,
                LockoutEndDateUtc = applicationUser.LockoutEndDateUtc,
                LockoutEnabled = applicationUser.LockoutEnabled,
                AccessFailedCount = applicationUser.AccessFailedCount,
                UserName = applicationUser.UserName
            });

            return Json(result);
        }

        public ActionResult Property_List(string Id)
        {
            if (Request.IsAjaxRequest()) {
                return PartialView();
            }
            return View();
        }

        public ActionResult PropertyDetail(int Id) {
            if (Request.IsAjaxRequest()) {

                return PartialView();
            
            }

            return View();
        }


        public ActionResult FirstName()
        {
           

            return Json(db.Users.Select(e => e.FirstName).Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LastName()
        {
           
            return Json(db.Users.Select(e => e.LastName).Distinct(), JsonRequestBehavior.AllowGet);
        } 

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
