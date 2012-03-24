using System.Web.Routing;

namespace Portfotolio.Site.Routing
{
    public class RequestOnlyRoute : Route
    {
        public RequestOnlyRoute(string url, IRouteHandler routeHandler) : base(url, routeHandler)
        {
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}