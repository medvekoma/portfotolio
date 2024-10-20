using Microsoft.AspNetCore.Mvc;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site4.Controllers
{
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult About()
        {
            ViewData[DataKeys.BreadCrumb] = "about";

            return View();
        }
    }
}
