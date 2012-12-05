using System.Web.Mvc;
using Portfotolio.Site.Services.Models;
using Portfotolio.Site4.Attributes;

namespace Portfotolio.Site4
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new HideFromSearchEnginesAttribute(AllowRobots.None, HideFromSearchEngineCondition.HasPageAttribute));
        }
    }
}