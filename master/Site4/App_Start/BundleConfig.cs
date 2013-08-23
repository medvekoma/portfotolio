using System.Web.Optimization;

namespace Portfotolio.Site4
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725

        const string JqueryCdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js"; //add link to jquery on the CDN

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/*.css"));
            bundles.Add(new ScriptBundle("~/Scripts/js").Include("~/Scripts/*.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Paging/js").Include("~/Scripts/Paging/*.js"));
            bundles.UseCdn = true;   //enable CDN support
            bundles.Add(new ScriptBundle("~/Scripts/jquery", JqueryCdnPath).Include("~/Scripts/jquery/jquery-{version}.js"));
        }
    }
}