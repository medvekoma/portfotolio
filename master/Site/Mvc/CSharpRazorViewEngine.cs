using System.Web.Mvc;
using System.Linq;

namespace Portfotolio.Site.Mvc
{
    public class CSharpRazorViewEngine : RazorViewEngine
    {
        public CSharpRazorViewEngine()
        {
            Initialize();
        }

        public CSharpRazorViewEngine(IViewPageActivator viewPageActivator) : base(viewPageActivator)
        {
            Initialize();
        }

        private void Initialize()
        {
            MasterLocationFormats = MasterLocationFormats.CSharpHtmlOnly();
            ViewLocationFormats = ViewLocationFormats.CSharpHtmlOnly();
            PartialViewLocationFormats = PartialViewLocationFormats.CSharpHtmlOnly();
            
            AreaMasterLocationFormats = AreaMasterLocationFormats.CSharpHtmlOnly();
            AreaPartialViewLocationFormats = AreaPartialViewLocationFormats.CSharpHtmlOnly();
            AreaViewLocationFormats = AreaViewLocationFormats.CSharpHtmlOnly();
        }
    }

    public static class HelperExtensions
    {
        public static string[] CSharpHtmlOnly(this string[] original)
        {
            return original
                .Where(item => item.EndsWith(".cshtml"))
                .ToArray();
        }
    }
}