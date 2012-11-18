using System.Web.Mvc;
using Portfotolio.Domain.Exceptions;
using Portfotolio.Domain.Persistency;
using Portfotolio.Services.Logging;
using Portfotolio.Site.Models;

namespace Portfotolio.Site.Helpers
{
    public class MasterHandleErrorAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var loggerFactory = DependencyResolver.Current.GetService<ILoggerFactory>();
            var logger = loggerFactory.GetLogger("MasterHandleError");
            logger.LogException(filterContext.Exception);

            var viewDataDictionary = new ViewDataDictionary();
            var userSession = DependencyResolver.Current.GetService<IUserSession>();
            viewDataDictionary[DataKeys.AuthenticationInfo] = userSession.GetAuthenticationInfo();
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
            filterContext.ExceptionHandled = true;
        }
    }
}