using System.Web;
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
            bundles.Add(new StyleBundle("~/Content/Admin").Include(
               "~/Content/bootstrap.css",
               "~/Content/AdminTheme/css/sb-admin-2.css",
               "~/Content/font-awesome.css",
               "~/Content/AdminTheme/css/metisMenu.css"));
            bundles.Add(new ScriptBundle("~/Scripts/AdminJs").Include(
                "~/Content/AdminTheme/js/sb-admin-2.js",
                "~/Content/AdminTheme/js/metisMenu.min.js", "~/Scripts/notify.min.js"));
            //Bundle For Kendo
            bundles.Add(new ScriptBundle("~/Scripts/Kendo").Include(
               "~/Scripts/kendo/js/jszip.min.js", 
               "~/Scripts/kendo/js/kendo.all.min.js",
                "~/Scripts/kendo/js/kendo.aspnetmvc.min.js"));
            bundles.Add(new StyleBundle("~/Content/Kendo").Include(
                "~/Content/kendo/css/kendo.common.min.css",
                "~/Content/kendo/css/kendo.blueopal.mobile.min.css",
                "~/Content/kendo/css/kendo.dataviz.min.css",
                 "~/Content/kendo/css/kendo.blueopal.min.css",
                "~/Content/kendo/css/kendo.dataviz.default.min.css"));
        }
    }
}
