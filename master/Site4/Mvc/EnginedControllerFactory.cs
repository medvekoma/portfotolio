using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Portfotolio.Site4.Mvc
{
    public class EnginedControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return (IController)GlobalConfiguration.Configuration.DependencyResolver.GetService(controllerType);
        }
    }
}