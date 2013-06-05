using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Portfotolio.DependencyInjection;

namespace Portfotolio.Site.DependencyInjection
{
    public class DependencyEngineWrapper : IDependencyResolver
    {
        private readonly IDependencyEngine _dependencyEngine;

        public DependencyEngineWrapper(IDependencyEngine dependencyEngine)
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