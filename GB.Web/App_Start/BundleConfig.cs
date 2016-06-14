using System.Web;
using System.Web.Optimization;

namespace GB.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/GBScripts").Include(
                        "~/Scripts/classie.js",
                        "~/Scripts/cbpAnimatedHeader.js",
                        "~/Scripts/jqBootstrapValidation.js",
                        "~/Scripts/contact_me.js",
                        "~/Scripts/agency.js"));
            bundles.Add(new ScriptBundle("~/bundles/googleanalytics").Include(
                       "~/Scripts/googleanalytics.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/css/bootstrap.min.css",
                        "~/Content/css/agency.css",
                        "~/Content/font-awesome/css/font-awesome.min.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));


            bundles.Add(new StyleBundle("~/Content/Careers").Include(
                        "~/Content/Custom/Careers.css"));
        }
    }
}