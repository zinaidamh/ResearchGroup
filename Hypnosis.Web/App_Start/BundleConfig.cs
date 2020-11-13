using System.Web;
using System.Web.Optimization;

namespace Hypnosis
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.IgnoreList.Clear();
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryValidate")
                .Include("~/Scripts/jquery.validate.js")
                .Include("~/Scripts/jquery.validate.unobtrusive.js")
                );
            bundles.Add(new ScriptBundle("~/bundles/jqueryValidateGlobalize")
                .Include("~/Scripts/jquery.validate.globalize.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryGlobalize")
                .Include("~/Scripts/globalize.js")
                .Include(string.Format("~/Scripts/globalize/cultures/globalize.culture.{0}.js", System.Threading.Thread.CurrentThread.CurrentUICulture.Name)));

            var scriptBundle = new ScriptBundle("~/bundles/all");
            scriptBundle.Include("~/Scripts/jquery-ui.js");
            scriptBundle.Include("~/Scripts/bootstrap.js");
            scriptBundle.Include("~/Scripts/respond.js");
            scriptBundle.Include("~/Scripts/bootstrap-datepicker.js");
            scriptBundle.Include("~/Scripts/bootstrap-select.js");
            scriptBundle.Include("~/Scripts/plugins/dataTables/jquery.dataTables.js");
            scriptBundle.Include("~/Scripts/plugins/dataTables/dataTables.bootstrap.js");
            scriptBundle.Include("~/Scripts/date.js");
            scriptBundle.Include("~/Scripts/select2.js");
            scriptBundle.Include("~/Scripts/jquery.ui.timepicker.js");
            scriptBundle.Include("~/Scripts/weekcalendar/jquery.weekcalendar.js");
            scriptBundle.Include("~/Scripts/weekcalendar/libs/jquery-ui-i18n.js");
            scriptBundle.Include("~/Scripts/jquery.qtip2/jquery.qtip.min.js");
            scriptBundle.Include("~/Scripts/vis.min.js");
            scriptBundle.Include("~/Scripts/jquery-loader/jquery-loader.js");
            scriptBundle.Include("~/Scripts/bootstrap-session-timeout.js");

            bundles.Add(scriptBundle);



            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"
                        ));


            var cssBundle = new StyleBundle("~/Content/css/bundle");
            if (System.Globalization.CultureInfo.CurrentCulture.TextInfo.IsRightToLeft)
            {
                cssBundle.Include("~/Content/css/bootstrap-rtl.css");
            }
            else
            {
                cssBundle.Include("~/Content/css/bootstrap.css");

            }
            cssBundle.Include("~/Content/css/themes/jquery-ui.css");
            cssBundle.Include("~/Content/css/select2.css");
            cssBundle.Include("~/Content/css/bootstrap-theme.css");
            cssBundle.Include("~/Content/css/bootstrap-datepicker.css");
            cssBundle.Include("~/Content/css/bootstrap-select.css");
            cssBundle.Include("~/Content/css/Site.css");
       
            cssBundle.Include("~/Scripts/weekcalendar/css/jquery.weekcalendar.css");
            cssBundle.Include("~/Scripts/jquery.qtip2/jquery.qtip.min.css");
            cssBundle.Include("~/Content/css/vis.min.css");
            cssBundle.Include("~/Scripts/jquery-loader/style.css");
            if (System.Globalization.CultureInfo.CurrentCulture.TextInfo.IsRightToLeft)
            {
                cssBundle.Include("~/Content/css/bootstrap-select-rtl.css");
            }
            cssBundle.Include("~/Content/css/plugins/dataTables/dataTables.bootstrap.css");
            if (System.Globalization.CultureInfo.CurrentCulture.TextInfo.IsRightToLeft)
            {
                cssBundle.Include("~/Content/css/plugins/dataTables/dataTables.bootstrap-rtl.css");
            }

               

               bundles.Add(cssBundle);
        }
    }
}