using System.Web.Mvc;
using System.Web.Routing;

namespace Portfotolio.Site4
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "home",
                url: "",
                defaults: new { controller = "photo", action = "interestingness" }
                );

            routes.MapRoute(
                name: "group",
                url: "group/{id}",
                defaults: new { controller = "photo", action = "group", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "photo",
                url: "{id}/{action}/{secondaryId}", 
                defaults: new { controller = "photo", action = "photos", secondaryId = UrlParameter.Optional },
                constraints: new { id = "^[^-].*" }
                );

            routes.MapRoute(
                name: "generic",
                url: "-{controller}/{action}/{id}",
                defaults: new { action = "show", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "catch-all",
                url: "{*url}",
                defaults: new { controller = "home", action = "incorrecturl" }
                );
        }
    }
}