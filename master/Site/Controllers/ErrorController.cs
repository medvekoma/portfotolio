using System;
using System.Web.Mvc;
using Portfotolio.Domain.Exceptions;
using Portfotolio.Domain.Persistency;
using Portfotolio.Services.Logging;
using Portfotolio.Site.Helpers;

namespace Portfotolio.Site.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger _logger;

        public ErrorController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.GetLogger("ErrorController");
        }

        public ActionResult Error(Exception exception)
        {
            _logger.LogException(exception);
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
