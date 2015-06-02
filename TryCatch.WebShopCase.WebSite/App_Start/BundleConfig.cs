using System.Web;
using System.Web.Optimization;

namespace TryCatch.WebShopCase.WebSite
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.9.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapJs").Include(
                        "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularJs").Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-sanitize.js"));

            bundles.Add(new ScriptBundle("~/bundles/mainAngularApp").Include(
                        "~/Scripts/Angular/app.main.js",
                       "~/Scripts/Angular/plugins/ngStorage.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/catalogScripts").Include(
                "~/Scripts/Angular/services/product.ang.service.js",
                "~/Scripts/Angular/controllers/catalog.ang.controller.js",               
                "~/Scripts/ui-bootstrap-tpls-0.13.0.js"
                         ));

            bundles.Add(new ScriptBundle("~/bundles/cartScripts").Include(
                "~/Scripts/Angular/services/cart.ang.service.js",
                "~/Scripts/Angular/services/product.ang.service.js",
                "~/Scripts/Angular/controllers/cart.ang.controller.js",                
                "~/Scripts/ui-bootstrap-tpls-0.13.0.js"
                         ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/mainThemeCss").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/bootstrap-theme.css",
                        "~/Content/themes/custom/Custom.css"
                        ));
        }
    }
}