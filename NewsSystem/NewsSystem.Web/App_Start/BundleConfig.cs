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
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new StyleBundle("~/Content/bootstrap-theme").Include(
                        "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/client-side-css").IncludeDirectory(
                        "~/Content/site", "*.css", true));

            bundles.Add(new StyleBundle("~/Content/AdminPanel/admin-bootstrap-theme-css").Include(
                        "~/Content/AdminPanel/bootstrap.min.css",
                        "~/Content/AdminPanel/sb-admin.css",
                        "~/Content/AdminPanel/plugins/morris.css"));

            bundles.Add(new StyleBundle("~/Content/AdminPanel/admin-side-css").IncludeDirectory(
                        "~/Content/AdminPanel/site", "*.css", true));
        }

        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
        }
    }
}
