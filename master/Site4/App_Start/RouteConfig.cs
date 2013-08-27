using System.Web.Mvc;
using System.Web.Routing;

namespace Portfotolio.Site4
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.RouteExistingFiles = true;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*url}", new { url = @"^.+\.[a-zA-Z]{2,4}$" });

            routes.MapRoute(
                name: "home",
                url: "",
                defaults: new { controller = "photo", action = "promotion" }
                );

            routes.MapRoute(
                name: "group",
                url: "group/{id}",
                defaults: new { controller = "photo", action = "group", id = UrlParameter.Optional },
                constraints: new { id = "^[0-9].*$"}
                );

            routes.MapRoute(
                name: "photo",
                url: "{id}/{action}/{secondaryId}", 
                defaults: new { controller = "photo", action = "photos", secondaryId = UrlParameter.Optional },
                constraints: new { id = "^[^-].*$" }
                );

            routes.MapRoute(
                name: "generic",
                url: "-{controller}/{action}/{id}",
                defaults: new { action = "show", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "catch-all",
                url: "{*url}",
                defaults: new { controller = "error", action = "incorrecturl" }
                );
        }
    }
}