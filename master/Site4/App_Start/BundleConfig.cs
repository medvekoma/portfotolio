using System.Web.Optimization;

namespace Portfotolio.Site4
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/*.css"));
            bundles.Add(new ScriptBundle("~/Scripts/js").Include("~/Scripts/*.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Paging/js").Include("~/Scripts/Paging/*.js"));
        }
    }
}