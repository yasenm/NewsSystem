namespace NewsSystem.Web
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScripts(bundles);
            RegisterStyles(bundles);

            BundleTable.EnableOptimizations = false;
        }

        private static void RegisterStyles(BundleCollection bundles)
        {
            RegisterCommonStyles(bundles);
            RegisterClientSideStyles(bundles);
            RegisterAdminPanelStyles(bundles);

        }

        private static void RegisterCommonStyles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-theme").Include(
                        "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/toastr").Include(
                        "~/Content/toastr.css"));
        }

        private static void RegisterClientSideStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/client-side-css").IncludeDirectory(
                        "~/Content/site", "*.css", true));
        }

        private static void RegisterAdminPanelStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/AdminPanel/admin-bootstrap-theme-css").Include(
                        "~/Content/AdminPanel/bootstrap.min.css",
                        "~/Content/AdminPanel/sb-admin.css",
                        "~/Content/AdminPanel/plugins/morris.css",
                        "~/Content/AdminPanel/plugins/fonts/font-awesome.css",
                        "~/Content/bootstrap-tokenfield/bootstrap-tokenfield.css",
                        "~/Content/bootstrap-tokenfield/bootstrap-typehead.css"));

            bundles.Add(new StyleBundle("~/Content/AdminPanel/admin-side-css").IncludeDirectory(
                        "~/Content/AdminPanel/site", "*.css", true));

            bundles.Add(new StyleBundle("~/Content/bootstrap-tokenfield").IncludeDirectory(
                        "~/Content/bootstrap-tokenfield", "*.css", true));

            bundles.Add(new StyleBundle("~/Content/full-jquery-ui").IncludeDirectory(
                        "~/Content/themes/base", "*.css", true));

            bundles.Add(new StyleBundle("~/Content/gridmvc").Include(
                        "~/Content/Gridmvc.css"));
        }

        private static void RegisterScripts(BundleCollection bundles)
        {
            RegisterCommonScripts(bundles);
            RegisterClientSideScripts(bundles);
            RegisterAdminPanelScripts(bundles);
        }

        private static void RegisterCommonScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                        "~/Scripts/jquery-ui-1.11.4.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-unobtrusive").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                        "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/common-scripts").Include(
                        "~/Scripts/common/app.js",
                        "~/Scripts/common/http-requester.js",
                        "~/Scripts/common/url-builder.js",
                        "~/Scripts/moment.min.js",
                        "~/Scripts/common/moment-js-settings-locale.js",
                        "~/Scripts/common/global.js"));

            //bundles.Add(new ScriptBundle("~/bundles/common-scripts").IncludeDirectory(
            //            "~/Scripts/common", "*.js", true));
        }

        private static void RegisterAdminPanelScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/admin-panel-bootstrap").Include(
                      "~/Scripts/AdminPanel/plugins/bootstrap.min.js",
                      "~/Scripts/AdminPanel/common/common-tags-js.js",
                      "~/Scripts/bootstrap-tokenfield.js"
                      //"~/Scripts/AdminPanel/plugins/morris/raphael.min.js",
                      //"~/Scripts/AdminPanel/plugins/morris/morris.js",
                      //"~/Scripts/AdminPanel/plugins/morris/morris-data.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/bootbox").Include(
                      "~/Scripts/bootbox.js"));

            bundles.Add(new ScriptBundle("~/bundles/tinymce").Include(
                      "~/Scripts/tinymce/tinymce.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin-panel-albums").Include(
                      "~/Scripts/AdminPanel/albums/albums.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin-panel-ns-images").Include(
                      "~/Scripts/AdminPanel/ns-images/ns-images.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin-panel-articles").Include(
                      "~/Scripts/AdminPanel/articles/articles.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin-panel-common-modal-triggers").Include(
                      "~/Scripts/AdminPanel/common/common-modal-triggers.js"));

            bundles.Add(new ScriptBundle("~/bundles/survey").Include(
                      "~/Scripts/AdminPanel/survey/survey.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/tinymce-settings").Include(
                        "~/Scripts/common/tinymce-initializer.js"));

            bundles.Add(new ScriptBundle("~/bundles/gridmvc").Include(
                      //"~/Scripts/ladda-bootstrap/*.min.js",
                      //"~/Scripts/URI.js",
                      //"~/Scripts/gridmvc-ext.js",
                      "~/Scripts/gridmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                      "~/Scripts/jquery-ui-1.11.4.min.js"));
        }

        private static void RegisterClientSideScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/client-side-bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/client/comments").Include(
                        "~/Scripts/client/comments/comments.js"));
        }
    }
}
