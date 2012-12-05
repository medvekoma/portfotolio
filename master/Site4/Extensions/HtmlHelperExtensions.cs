using System.Web.Mvc;
using Portfotolio.Domain.Persistency;
using Portfotolio.Site.Services.Models;

namespace Portfotolio.Site4.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string UserIdentifier(this HtmlHelper htmlHelper)
        {
            var session = htmlHelper.ViewContext.HttpContext.Session;
            if (session == null)
                return null;
            return session[DataKeys.UserIdentifier] as string;
        }

        public static MvcHtmlString MetaRobots(this HtmlHelper htmlHelper)
        {
            object allowRobotsObject = htmlHelper.ViewData[DataKeys.AllowRobots];
            if (allowRobotsObject == null || !(allowRobotsObject is AllowRobots))
                return new MvcHtmlString(string.Empty);

            var allowRobots = (AllowRobots)allowRobotsObject;
            var elements = new[] { "noindex", "nofollow", "noarchive" };
            if ((allowRobots & AllowRobots.Index) == AllowRobots.Index)
                elements[0] = "index";
            if ((allowRobots & AllowRobots.Follow) == AllowRobots.Follow)
                elements[1] = "follow";
            if ((allowRobots & AllowRobots.Archive) == AllowRobots.Archive)
                elements[1] = "archive";

            string content = string.Join(", ", elements);
            string metaTag = string.Format("<meta name='robots' content='{0}' />", content);
            return new MvcHtmlString(metaTag);
        }
    }
}