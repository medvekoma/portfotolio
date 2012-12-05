using System.Web.Mvc;

namespace Portfotolio.Site4.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string NextPageAction(this UrlHelper urlHelper, int page)
        {
            var routeValues = urlHelper.RequestContext.RouteData.Values;
            var actionName = routeValues["action"] as string;

            var nameValueCollection = urlHelper.RequestContext.HttpContext.Request.QueryString;
            foreach (string key in nameValueCollection.AllKeys)
            {
                routeValues[key] = nameValueCollection[key];
            }
            routeValues["page"] = page;

            return urlHelper.Action(actionName, routeValues);
        }
    }
}