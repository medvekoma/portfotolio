using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Portfotolio.Site.DependencyInjection
{
    public class ControllerActivator : IControllerActivator
    {
        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return (IController)DependencyResolver.Current.GetService(controllerType);
        }
    }
}