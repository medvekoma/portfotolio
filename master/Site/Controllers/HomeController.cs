using System.Web.Mvc;
using Portfotolio.Site.Helpers;

namespace Portfotolio.Site.Controllers
{
    public class HomeController : Controller
    {
        [BreadCrumb("about")]
        public ActionResult About()
        {
            return View();
        }
    }
}
