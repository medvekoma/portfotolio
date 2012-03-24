using System;
using System.Collections.Generic;

namespace Portfotolio.Utility.DependencyInjection
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
            where TImplementation : TInterface;
        void Register<TImplementation>(DependencyLifeStyle dependencyLifeStyle);
        void Register<TImplementation>(TImplementation instance, DependencyLifeStyle dependencyLifeStyle);

        void RegisterAndDecorate<TInterface, TDecorated, TDecorator>(DependencyLifeStyle dependencyLifeStyle)
            where TDecorated : TInterface
            where TDecorator : TInterface;

        bool HasService(Type serviceType);
        object GetService(Type serviceType);
        IEnumerable<object> GetServices(Type serviceType);
    }
}