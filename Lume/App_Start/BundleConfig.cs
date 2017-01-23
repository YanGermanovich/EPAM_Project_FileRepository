using System.Web;
using System.Web.Optimization;

namespace Lume
{
    public class BundleConfig
    {
        // Дополнительные сведения о Bundling см. по адресу http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                        "~/Scripts/myScripts/bootstrap-min.js"));
            bundles.Add(new ScriptBundle("~/bundles/authenticated").Include(

                        "~/Scripts/myScripts/common.js"));
            bundles.Add(new ScriptBundle("~/bundles/indexJS").Include(
                        "~/Scripts/myScripts/jquery.dataTables-min.js",
                        "~/Scripts/myScripts/star-rating-min.js",
                        "~/Scripts/myScripts/bootstrap-select-min.js",
                        "~/Scripts/myScripts/dataTables.bootstrap-min.js",
                        "~/Scripts/myScripts/fileinput-min.js",
                        "~/Scripts/myScripts/index.js"));
            bundles.Add(new ScriptBundle("~/bundles/searchJS").Include(
                        "~/Scripts/myScripts/jquery.dataTables-min.js",
                        "~/Scripts/myScripts/star-rating-min.js",
                        "~/Scripts/myScripts/search.js"));


            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство построения на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/myThemes/bootstrap-theme-min.css",
                        "~/Content/myThemes/bootstrap-min.css",
                        "~/Content/myThemes/common.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
            bundles.Add(new StyleBundle("~/Content/themes/index").Include(
                        "~/Content/myThemes/dataTables.bootstrap-min.css",
                        "~/Content/myThemes/star-rating-min.css",
                        "~/Content/myThemes/modal_slide.css",
                        "~/Content/myThemes/bootstrap-select-min.css",
                        "~/Content/myThemes/fileinput-min.css",
                        "~/Content/myThemes/index.css"));
            bundles.Add(new StyleBundle("~/Content/themes/search").Include(
                        "~/Content/myThemes/search.dataTables-min.css",
                        "~/Content/myThemes/star-rating-min.css",
                        "~/Content/myThemes/modal_slide.css",
                        "~/Content/myThemes/search.css"));
        }
    }
}