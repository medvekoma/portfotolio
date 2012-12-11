using System;
using System.Diagnostics;
using System.Web.Mvc;
using Portfotolio.Domain.Persistency;
using Portfotolio.Services.Caching;
using Portfotolio.Site.Services.Models;
using Portfotolio.Site4.Attributes;

namespace Portfotolio.Site4.Controllers
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
            var sessionCount = ControllerContext.HttpContext.Application[DataKeys.SessionCount];
            var elementsInCache = _cacheProvider.GetCacheSize();
            var gcTotalMemory = GC.GetTotalMemory(false) >> 20;
            var currentProcess = Process.GetCurrentProcess();
            long workingSet = currentProcess.WorkingSet64 >> 20;
            long privateMemory = currentProcess.PrivateMemorySize64 >> 20;
            var model = new TestModel
                            {
                                StartedOn = startedOn,
                                ElementsInCache = elementsInCache,
                                SessionCount = sessionCount,
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

        public ActionResult Log(string id)
        {
            int offset;
            if (!Int32.TryParse(id, out offset))
                offset = 0;
            var fileName = "~/logs/" + DateTime.UtcNow.AddDays(-offset).ToString("yyyy-MM-dd") + ".log";

            return File(fileName, "text/plain");
        }
    }
}