using System.Web.Mvc;
using Portfotolio.Domain.Exceptions;

namespace Portfotolio.Site4.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult IncorrectUrl()
        {
            throw new IncorrectUrlException();
        }
    }
}
