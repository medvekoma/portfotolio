using System.Web.Optimization;

namespace Portfotolio.Site4
{
	// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
	public class BundleConfig
    {
        const string JqueryCdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js";

        public static void RegisterBundles(BundleCollection bundles)
        {
			bundles.UseCdn = true;
			bundles.Add(new StyleBundle("~/bundles/css").Include("~/Content/*.css"));
            bundles.Add(new ScriptBundle("~/bundles/js").Include("~/Scripts/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/paging").Include("~/Scripts/Paging/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery", JqueryCdnPath).Include("~/Scripts/jquery/jquery-{version}.js"));
        }
    }
}