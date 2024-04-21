using System.Web.Optimization;

namespace eUseControl.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {


            // Bootstrap style
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include(
                      "~/Contents/css/open-iconic-bootstrap.min.css",
                      "~/Contents/css/animate.css",
                      "~/Contents/css/owl.carousel.min.css",
                      "~/Contents/css/owl.theme.default.min.css",
                      "~/Contents/css/magnific-popup.css",
                      "~/Contents/css/aos.css",
                      "~/Contents/css/ionicons.min.css",
                      "~/Contents/css/flaticon.css",
                      "~/Contents/css/icomoon.css",
                      "~/Contents/css/style.css"
                      ));

            // Bootstrap
            bundles.Add(new Bundle("~/bundles/bootstrap/js").Include(
                      "~/Scripts/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jQuery.3.7.1/Content/Scripts/jquery-{version}.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                      "~/Scripts/js/popper.min.js",
                      "~/Scripts/js/owl.carousel.min.js",
                      "~/Scripts/js/aos.js",
                      "~/Scripts/js/scrollax.min.js",
                      "~/Scripts/js/google-map.js",
                      "~/Scripts/js/main.js"
                      ));
        }
    }
}