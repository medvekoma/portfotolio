using System;
using System.Web;
using System.Web.Mvc;
using NLog;
using Portfotolio.DependencyInjection;
using Portfotolio.Domain.Persistency;
using Portfotolio.Site.Mvc;
using Portfotolio.Site.Services.Logging;

namespace Portfotolio.Site
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        private readonly Logger _logger;
        private IDependencyEngine _dependencyEngine;

        public MvcApplication()
        {
            _logger = LogManager.GetLogger("Application");
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
            _logger.LogException(exception);

            Server.Transfer("/Content/error.htm");
        }
    }
}