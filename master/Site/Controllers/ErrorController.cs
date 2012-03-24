using System;
using System.Web.Mvc;
using NLog;
using Portfotolio.Domain.Exceptions;
using Portfotolio.Domain.Persistency;
using Portfotolio.Site.Helpers;

namespace Portfotolio.Site.Controllers
{
    public class ErrorController : Controller
    {
        private static readonly Logger Logger = LogManager.GetLogger("ErrorController");

        public ActionResult Error(Exception exception)
        {
            Logger.LogException(exception);
            var portfotolioException = exception as PortfotolioException;
            string errorMessage = null;
            int statusCode = 500;
            if (portfotolioException != null)
            {
                errorMessage = portfotolioException.Message;
                statusCode = portfotolioException.HttpStatusCode;
            }
            ViewData[DataKeys.ErrorMessage] = errorMessage;
            Response.StatusCode = statusCode;
            
            return View();
        }
    }
}
