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
                defaults: new { controller = "photo", action = "photos", id = "serkansozer" }
                );

            routes.MapRoute(
                name: "photo",
                url: "{id}/{action}/{secondaryId}", 
                defaults: new { controller = "photo", action = "photos", secondaryId = UrlParameter.Optional },
                constraints: new { id = "serkansozer" }
                );

            routes.MapRoute(
                name: "exif",
                url: "{action}/{photoid}",
                defaults: new { controller = "photo", action = "BasicExifData", secondaryId = UrlParameter.Optional },
                constraints: new { action = "BasicExifData" }
                );

            routes.MapRoute(
                name: "catch-all",
                url: "{*url}",
                defaults: new { controller = "error", action = "incorrecturl" }
                );
        }
    }
}