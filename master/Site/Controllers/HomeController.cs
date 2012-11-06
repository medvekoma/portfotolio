using System;
using System.Web.Mvc;
using Portfotolio.Domain.Persistency;
using Portfotolio.Services.Caching;
using Portfotolio.Site.Helpers;

namespace Portfotolio.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICacheProvider _cacheProvider;

        public HomeController(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        [BreadCrumb("about")]
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Test()
        {
            var appStarted = (DateTime) ControllerContext.HttpContext.Application[DataKeys.ApplicationStarted];
            var cacheSize = _cacheProvider.GetCacheSize();
            string message = string.Format("App started on: {0}<br>Number of elements in cache: {1}", appStarted, cacheSize);
            return Content(message);
        }
    }
}
