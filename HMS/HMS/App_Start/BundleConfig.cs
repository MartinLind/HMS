using System.Web;
using System.Web.Optimization;

namespace HMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.1.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Verwenden Sie die Entwicklungsversion von Modernizr zum Entwickeln und Erweitern Ihrer Kenntnisse. Wenn Sie dann
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            "~/Scripts/jquery-ui-1.12.1.min.js"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").IncludeDirectory(
            "~/Content/themes/base", "*.css", true));

            bundles.Add(new ScriptBundle("~/bundles/jQWidgets").Include(
            "~/Scripts/jQWidgets/jqx-all.js",
            "~/Scripts/jQWidgets/globalization/globalize.js",
            "~/Scripts/jQWidgets/globalization/globalize.culture.de-DE.js"));

            bundles.Add(new StyleBundle("~/Content/JQWidget/css").Include(
                        "~/Content/JQWidget/jqx.base.css",
                        "~/Content/JQWidget/jqx.classic.css"));


        }
    }
}
