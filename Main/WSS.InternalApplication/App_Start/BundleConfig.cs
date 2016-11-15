using System.Web.Optimization;

namespace WSS.InternalApplication
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/ISTBaseBundle").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/toastr.js",
                "~/Scripts/UserNotifications.js",
                "~/Scripts/bootstrap-confirm-button.js",
                "~/Scripts/InternalAppScripts.js", 
                "~/Scripts/bootstrap-datepicker.min.js"
                ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css",
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css",
                "~/Content/font-awesome.css",
                "~/Content/bootstrap-datepicker3.min.css"
                ));

            bundles.Add(new StyleBundle("~/content/toastr",
                "http://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css")
                .Include("~/Content/toastr.css"));

            bundles.Add(new ScriptBundle("~/bundles/toastr",
                "http://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js")
                .Include("~/Scripts/toastr.js"));
        }
    }
}