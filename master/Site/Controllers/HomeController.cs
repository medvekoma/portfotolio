using System.Web.Mvc;
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

        public ActionResult CacheSize()
        {
            var cacheSize = _cacheProvider.GetCacheSize();
            return Content("Number of elements in cache: " + cacheSize);
        }
    }
}
