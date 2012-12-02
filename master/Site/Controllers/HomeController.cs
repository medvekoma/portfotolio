using System.Web.Mvc;
using Portfotolio.Domain.Exceptions;
using Portfotolio.Domain.Persistency;
using Portfotolio.Site.Helpers;
using Portfotolio.Site.Models;

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

        public ActionResult Home()
        {
            var homeModel = new HomeModel("", SearchSource.People);
            return View(homeModel);
        }

        [HttpPost]
        public ActionResult Home(HomeModel homeModel)
        {
            return Content(homeModel.SearchText + " > " + homeModel.SearchSource);
        }

        public ActionResult IncorrectUrl()
        {
            throw new IncorrectUrlException();
        }
    }
}
