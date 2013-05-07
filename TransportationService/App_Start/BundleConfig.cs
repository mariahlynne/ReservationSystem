using System.Web;
using System.Web.Optimization;

namespace MvcApplication1
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
               "~/Scripts/jquery-1.9.1.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/ReservationSystem").Include(
               "~/Scripts/jquery.transit.js",
               "~/Scripts/Logon.js",
               "~/Scripts/userManager.js",
               "~/Scripts/notifier.js",
               "~/Scripts/roll_utility.js",
               "~/Scripts/bootstrap.js",
               "~/Scripts/AdminActions.js",
               "~/Scripts/addActions.js",
               "~/Scripts/modifyActions.js",
               "~/Scripts/viewActions.js",
               "~/Scripts/jquery-ui-1.10.2.js",
               "~/Scripts/messageBuilder.js",
               "~/Scripts/jquery.tablesorter.js"
               //"~/Scripts/jquery-ui-timepicker-addon.js"
               //"~/Scripts/jquery-TimePicker-1.0.0.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
               "~/Content/site.css",
               "~/Content/utility.css",
               "~/Content/bootstrap.css",
               "~/Content/font-awesome.css"
            ));
        }
    }
}
