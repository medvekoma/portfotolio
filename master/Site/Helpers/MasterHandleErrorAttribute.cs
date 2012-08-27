using System.Web.Mvc;
using NLog;
using Portfotolio.Domain.Exceptions;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site.Helpers
{
    public class MasterHandleErrorAttribute : FilterAttribute, IExceptionFilter
    {
        private static readonly Logger Logger = LogManager.GetLogger("MasterHandleError");

        public void OnException(ExceptionContext filterContext)
        {
            Logger.LogException(filterContext.Exception);

            var viewDataDictionary = new ViewDataDictionary();
            var userSession = DependencyResolver.Current.GetService<IUserSession>();
            viewDataDictionary[DataKeys.AuthenticationInfo] = userSession.GetAuthenticationInfo();

            string errorMessage = null;
            int statusCode = 500;
            var portfotolioException = filterContext.Exception as PortfotolioException;
            if (portfotolioException != null)
            {
                errorMessage = portfotolioException.Message;
                statusCode = portfotolioException.HttpStatusCode;
            }

            viewDataDictionary[DataKeys.ErrorMessage] = errorMessage;

            var viewResult = new ViewResultWithStatusCode(statusCode, errorMessage)
                                 {
                                     ViewData = viewDataDictionary, 
                                     ViewName = "Error",
                                 };
            filterContext.Result = viewResult;
            filterContext.ExceptionHandled = true;
        }
    }
}