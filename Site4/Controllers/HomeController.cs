using System.Web.Mvc;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            ViewData[DataKeys.BreadCrumb] = "about";

            return View();
        }
    }
}
