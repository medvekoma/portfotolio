using System.Web.Mvc;
using Portfotolio.Domain.Persistency;
using Portfotolio.Site.Helpers;

namespace Portfotolio.Site.Controllers
{
    public class HomeController : Controller
    {
        [RememberActionUrl]
        public ActionResult About()
        {
            ViewData[DataKeys.BreadCrumb] = "about";

            return View();
        }
    }
}
