﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ELand.Models;

namespace ELand.Areas.Admin.Controllers
{
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
