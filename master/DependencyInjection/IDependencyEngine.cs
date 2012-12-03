using System;
using System.Collections.Generic;

namespace Portfotolio.DependencyInjection
{
    public enum DependencyLifeStyle
    {
        Singleton,
        PerWebRequest,
        Transient,
    }

    public interface IDependencyEngine : IDisposable
    {
        void Register<TInterface, TImplementation>(DependencyLifeStyle dependencyLifeStyle)
            where TInterface : class
            where TImplementation : TInterface;

        void Register<TImplementation>(DependencyLifeStyle dependencyLifeStyle)
            where TImplementation : class;

        void Register<TImplementation>(TImplementation instance, DependencyLifeStyle dependencyLifeStyle)
            where TImplementation: class;

        void RegisterAndDecorate<TInterface, TDecorated, TDecorator>(DependencyLifeStyle dependencyLifeStyle)
            where TInterface : class
            where TDecorated : TInterface
            where TDecorator : TInterface;

        bool HasService(Type serviceType);
        object GetService(Type serviceType);
        IEnumerable<object> GetServices(Type serviceType);
    }
}