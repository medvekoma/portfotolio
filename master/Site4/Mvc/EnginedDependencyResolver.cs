using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Portfotolio.DependencyInjection;

namespace Portfotolio.Site4.Mvc
{
    public class EnginedDependencyResolver : IDependencyResolver
    {
        private readonly IDependencyEngine _dependencyEngine;

        public EnginedDependencyResolver(IDependencyEngine dependencyEngine)
        {
            _dependencyEngine = dependencyEngine;
        }

        public object GetService(Type serviceType)
        {
            return _dependencyEngine.HasService(serviceType)
                       ? _dependencyEngine.GetService(serviceType)
                       : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _dependencyEngine.HasService(serviceType)
                       ? _dependencyEngine.GetServices(serviceType)
                       : new object[0];
        }
    }
}