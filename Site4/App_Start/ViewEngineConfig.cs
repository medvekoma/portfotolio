using System.Web.Mvc;

namespace Portfotolio.Site4
{
    public class ViewEngineConfig
    {
        public static void Register()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}