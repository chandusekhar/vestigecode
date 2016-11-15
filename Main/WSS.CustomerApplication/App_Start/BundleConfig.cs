using System.Web.Optimization;

namespace WSS.CustomerApplication
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
              "~/Scripts/CustomerAppScripts.js",
              "~/Scripts/bootstrap-confirm-button.js",
              "~/Scripts/bootstrap-datepicker.js",
              "~/Scripts/jquery.dirtyforms.js",
              "~/Scripts/modernizr-*"
              ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css",
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css",
                "~/Content/font-awesome.css",
                "~/Content/toastr.css"));
        }
    }
}