using System;
using System.Diagnostics;
using System.Web.Mvc;
using Portfotolio.Domain.Persistency;
using Portfotolio.Services.Caching;
using Portfotolio.Site.Helpers;
using Portfotolio.Site.Models;

namespace Portfotolio.Site.Controllers
{
    [HideFromSearchEngines(AllowRobots.None)]
    public class TestController : Controller
    {
        private readonly ICacheProvider _cacheProvider;

        public TestController(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public ActionResult Show()
        {
            var startedOn = ControllerContext.HttpContext.Application[DataKeys.ApplicationStarted];
            var elementsInCache = _cacheProvider.GetCacheSize();
            var gcTotalMemory = GC.GetTotalMemory(false) / 1024;
            var currentProcess = Process.GetCurrentProcess();
            long workingSet = currentProcess.WorkingSet64 / 1024;
            long privateMemory = currentProcess.PrivateMemorySize64 / 1024;
            var model = new TestModel
            {
                StartedOn = startedOn,
                ElementsInCache = elementsInCache,
                GcTotalMemory = gcTotalMemory,
                WorkingSet = workingSet,
                PrivateMemory = privateMemory,
            };
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Show(string dummy)
        {
            GC.Collect(2);
            return RedirectToAction("Show");
        }
    }
}