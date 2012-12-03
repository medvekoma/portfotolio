using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Portfotolio.DependencyInjection.WindsorCastle
{
    public class WindsorDependencyEngine : IDependencyEngine
    {
        private readonly IWindsorContainer _windsorContainter;
        private static readonly IDictionary<DependencyLifeStyle, LifestyleType> LifestyleMap;

        static WindsorDependencyEngine()
        {
            LifestyleMap = new Dictionary<DependencyLifeStyle, LifestyleType>()
                               {
                                   {DependencyLifeStyle.Singleton, LifestyleType.Singleton},
                                   {DependencyLifeStyle.PerWebRequest, LifestyleType.PerWebRequest},
                                   {DependencyLifeStyle.Transient, LifestyleType.Transient},
                               };
        }

        public WindsorDependencyEngine()
        {
            _windsorContainter = new WindsorContainer();
        }

        public void Register<TInterface, TImplementation>(DependencyLifeStyle dependencyLifeStyle)
            where TInterface : class 
            where TImplementation : TInterface
        {
            var lifestyleType = LifestyleMap[dependencyLifeStyle];
            _windsorContainter.Register(Component.For<TInterface>().ImplementedBy<TImplementation>().LifeStyle.Is(lifestyleType)); // .Named(typeof(TInterface).FullName)
        }

        public void Register<TImplementation>(DependencyLifeStyle dependencyLifeStyle)
            where TImplementation: class 
        {
            var lifestyleType = LifestyleMap[dependencyLifeStyle];
            _windsorContainter.Register(Component.For<TImplementation>().LifeStyle.Is(lifestyleType));
        }

        public void Register<TImplementation>(TImplementation instance, DependencyLifeStyle dependencyLifeStyle)
            where TImplementation : class
        {
            var lifestyleType = LifestyleMap[dependencyLifeStyle];
            _windsorContainter.Register(Component.For<TImplementation>().Instance(instance).LifeStyle.Is(lifestyleType));
        }

        public void RegisterAndDecorate<TInterface, TDecorated, TDecorator>(DependencyLifeStyle dependencyLifeStyle)
            where TInterface : class
            where TDecorated : TInterface 
            where TDecorator : TInterface
        {
            Register<TInterface, TDecorator>(dependencyLifeStyle);
            Register<TInterface, TDecorated>(dependencyLifeStyle);
        }

        public bool HasService(Type serviceType)
        {
            return _windsorContainter.Kernel.HasComponent(serviceType);
        }

        public object GetService(Type serviceType)
        {
            return _windsorContainter.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _windsorContainter.ResolveAll(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            _windsorContainter.Dispose();
        }
    }
}