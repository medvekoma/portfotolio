using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NLog;
using Portfotolio.Site.Controllers;
using Portfotolio.Site.DependencyInjection;
using Portfotolio.Site.Mvc;
using Portfotolio.Utility.DependencyInjection;

namespace Portfotolio.Site
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        private readonly Logger _logger = LogManager.GetLogger("Global");

        private IDependencyEngine _dependencyEngine;

        protected void Application_Start()
        {
            _logger.Info("Application Started.");
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

            IController errorController = new ErrorController();
            var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
            errorController.Execute(rc);
        }
    }
}