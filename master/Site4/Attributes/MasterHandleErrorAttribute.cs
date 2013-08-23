using System.Web.Mvc;
using Portfotolio.Domain.Exceptions;
using Portfotolio.Domain.Persistency;
using Portfotolio.Services.Logging;
using Portfotolio.Site.Services.Logging;
using Portfotolio.Site.Services.Models;
using Portfotolio.Site4.Extensions;

namespace Portfotolio.Site4.Attributes
{
    public class MasterHandleErrorAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var loggerFactory = DependencyResolver.Current.GetService<ILoggerProvider>();
            var logger = loggerFactory.GetLogger("MasterHandleError");
            logger.LogException(filterContext.Exception);

            var viewDataDictionary = new ViewDataDictionary();
            viewDataDictionary[DataKeys.AllowRobots] = AllowRobots.None;

            string errorMessage = null;
            var portfotolioException = filterContext.Exception as PortfotolioException;
            if (portfotolioException != null)
            {
                errorMessage = portfotolioException.Message;
            }

            viewDataDictionary.Model = new ModelError(errorMessage);

            var viewResult = new ViewResult
                                 {
                                     ViewData = viewDataDictionary, 
                                     ViewName = "Error",
                                 };
            filterContext.Result = viewResult;
            filterContext.HttpContext.Response.SetHttpHeader(filterContext.Exception);
            filterContext.ExceptionHandled = true;
        }
    }
}