using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Portfotolio.Domain.Persistency;
using Portfotolio.Services.Logging;
using Portfotolio.Site.Controllers;
using Portfotolio.Site.Mvc;
using Portfotolio.Utility.DependencyInjection;

namespace Portfotolio.Site
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        private readonly ILoggerFactory _loggerFactory = new NLogLoggerFactory();
        private readonly ILogger _logger;
        private IDependencyEngine _dependencyEngine;

        public MvcApplication()
        {
            _logger = _loggerFactory.GetLogger("global");
        }

        protected void Application_Start()
        {
            _logger.Info("Application Started.");
            HttpContext.Current.Application[DataKeys.ApplicationStarted] = DateTime.Now;
            AreaRegistration.RegisterAllAreas();

            _dependencyEngine = MvcConfigurator.RegisterDependencyInjectionFramework();

            MvcConfigurator.RegisterViewEngines();
            MvcConfigurator.RegisterGlobalFilters();
            MvcConfigurator.RegisterRoutes();
        }

        protected void Application_End()
        {
            _logger.Info("Application Stopped.");
            _dependencyEngine.Dispose();
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            Response.Clear();
            Server.ClearError();
            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = "Error";
            routeData.Values["exception"] = exception;

            IController errorController = new ErrorController(_loggerFactory);
            var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
            errorController.Execute(rc);
        }
    }
}