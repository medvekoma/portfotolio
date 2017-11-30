#define CDN
using System.Web.Optimization;

namespace Portfotolio.Site4
{
	// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
	public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
			bundles.Add(new StyleBundle("~/bundles/css").Include("~/Content/*.css"));
            bundles.Add(new ScriptBundle("~/bundles/js").Include("~/Scripts/*.js"));
			bundles.Add(new ScriptBundle("~/bundles/paging").Include("~/Scripts/Paging/*.js"));

#if CDN
			bundles.UseCdn = true;
			const string jqueryCdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.2.min.js";
			bundles.Add(new ScriptBundle("~/bundles/jquery", jqueryCdnPath).Include("~/Scripts/jquery/jquery-{version}.js"));
#else
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery/jquery-{version}.js"));
#endif
        }
    }
}