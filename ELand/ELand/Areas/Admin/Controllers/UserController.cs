﻿﻿using System;
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
            return View();
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}