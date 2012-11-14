using System;
using System.Diagnostics;
using System.Web.Mvc;
using Portfotolio.Domain.Persistency;
using Portfotolio.Services.Caching;

namespace Portfotolio.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICacheProvider _cacheProvider;

        public HomeController(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        // [BreadCrumb("about")]
        public ActionResult About()
        {
            ViewData[DataKeys.BreadCrumb] = "about";

            return View();
        }

        public ActionResult Test()
        {
            var startedOn = ControllerContext.HttpContext.Application[DataKeys.ApplicationStarted];
            var elementsInCache = _cacheProvider.GetCacheSize();
            var gcTotalMemory = GC.GetTotalMemory(false) / 1024;
            long workingSet64 = Process.GetCurrentProcess().WorkingSet64 / 1024;
            var model = new TestModel
                {
                    StartedOn = startedOn, 
                    ElementsInCache = elementsInCache, 
                    GcTotalMemory = gcTotalMemory,
                    WorkingSet = workingSet64,
                };
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Test(string dummy)
        {
            GC.Collect(2);
            return RedirectToAction("Test");
        }
    }

    public class TestModel
    {
        public object StartedOn { get; set; }

        public long ElementsInCache { get; set; }

        public long GcTotalMemory { get; set; }

        public long WorkingSet { get; set; }
    }
}
