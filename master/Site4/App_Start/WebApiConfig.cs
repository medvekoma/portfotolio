using System.Web.Http;
using Portfotolio.DependencyInjection;
using Portfotolio.DependencyInjection.Unity;
using Portfotolio.Site4.Mvc;

namespace Portfotolio.Site4
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
