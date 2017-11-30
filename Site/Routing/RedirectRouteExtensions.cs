using System.Web;
using System.Web.Routing;

namespace Portfotolio.Site.Routing
{
    public static class RedirectRouteExtensions
    {
        public static Route RedirectPermanently(this RouteCollection routes, string url, string targetUrl, object defaults)
        {
            var route = new RequestOnlyRoute(url, new DelegateRouteHandler(context => GetRedirectHandler(context, targetUrl, true)))
                            {
                                Defaults = new RouteValueDictionary(defaults)
                            };
            routes.Add(route);
            return route;
        }

        private static IHttpHandler GetRedirectHandler(RequestContext requestContext, string targetUrl, bool permanently)
        {
            string prefix = string.Empty;
            var slashPosition = targetUrl.IndexOf('/');
            if (slashPosition >= 0)
            {
                prefix = targetUrl.Substring(0, slashPosition + 1);
                targetUrl = targetUrl.Substring(slashPosition + 1);

            }
            var route = new Route(targetUrl, null);
            var virtualPathData = route.GetVirtualPath(requestContext, requestContext.RouteData.Values);
            if (virtualPathData != null)
            {
                targetUrl = prefix + virtualPathData.VirtualPath;
            }
            return new DelegateHttpHandler(httpContext => Redirect(httpContext, targetUrl, permanently), false);
        }

        private static void Redirect(HttpContext context, string url, bool permanently)
        {
            if (permanently)
            {
                context.Response.Status = "301 Moved Permanently";
                context.Response.StatusCode = 301;
                context.Response.AddHeader("Location", url);
            }
            else
            {
                context.Response.Redirect(url, false);
            }
        }
    }
}