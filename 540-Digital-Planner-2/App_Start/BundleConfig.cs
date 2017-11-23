using System.Web;
using System.Web.Optimization;

namespace Digital_Planner
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-*"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/respond").Include(
                      "~/Scripts/respond.js",
                      "~/Scripts/respond.matchmedia*"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                      "~/Scripts/moment.js"//,
                      //"~/Scripts/moment-*"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-datetimepicker.css",
                     // "~/Content/bootstrap-*",
                      "~/Content/Site.css"));
        }
    }
}
