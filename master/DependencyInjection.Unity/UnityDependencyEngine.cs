using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace Portfotolio.DependencyInjection.Unity
{
    public class UnityDependencyEngine : IDependencyEngine
    {
        private readonly IUnityContainer _container;

        public UnityDependencyEngine()
        {
            _container = new UnityContainer();
            var unityDecoratorExtension = new UnityDecoratorExtension();
            _container.AddExtension(unityDecoratorExtension);
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public void Register<TInterface, TImplementation>(DependencyLifeStyle dependencyLifeStyle)
            where TInterface : class where TImplementation : TInterface
        {
            var lifetimeManager = LifetimeMap(dependencyLifeStyle);
            _container.RegisterType<TInterface, TImplementation>(lifetimeManager);
        }

        public void Register<TImplementation>(DependencyLifeStyle dependencyLifeStyle) where TImplementation : class
        {
            var lifetimeManager = LifetimeMap(dependencyLifeStyle);
            _container.RegisterType<TImplementation>(lifetimeManager);
        }

        public void Register<TImplementation>(TImplementation instance, DependencyLifeStyle dependencyLifeStyle)
            where TImplementation : class
        {
            var lifetimeManager = LifetimeMap(dependencyLifeStyle);
            _container.RegisterInstance(typeof(TImplementation), instance, lifetimeManager);
        }

        public void RegisterAndDecorate<TInterface, TDecorated, TDecorator>(DependencyLifeStyle dependencyLifeStyle)
            where TInterface : class where TDecorated : TInterface where TDecorator : TInterface
        {
            _container.RegisterType<TInterface, TDecorator>(LifetimeMap(dependencyLifeStyle));
            _container.RegisterType<TInterface, TDecorated>(LifetimeMap(dependencyLifeStyle));
        }

        public bool HasService(Type serviceType)
        {
            return _container.IsRegistered(serviceType);
        }

        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.ResolveAll(serviceType);
        }

        private static LifetimeManager LifetimeMap(DependencyLifeStyle lifeStyle)
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    return new TransientLifetimeManager();
                case DependencyLifeStyle.PerWebRequest:
                    return new UnityPerWebRequestLifetimeManager();
                case DependencyLifeStyle.Singleton:
                    return new ContainerControlledLifetimeManager();
                default:
                    throw new NotSupportedException("Lifestyle not supported: " + lifeStyle);
            }
        }
    }
}
