﻿using System.Web;
using System.Web.Optimization;

namespace ELand
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/js/modernizr.custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Theme/style.css"));
            bundles.Add(new ScriptBundle("~/Scripts/Myfiles").Include(
                "~/Scripts/js/dependencies.js",
                "~/Scripts/js/select2/select2.min.js",
                "~/Scripts/js/slider-bootstrap/bootstrap-slider.js",
                "~/Scripts/js/contact-form.js",
                "~/Scripts/js/ct-mediaSection/jquery.stellar.min.js",
                "~/Scripts/js/owl/owl.carousel.min.js",
                "~/Scripts/js/main.js"));
        }
    }
}
