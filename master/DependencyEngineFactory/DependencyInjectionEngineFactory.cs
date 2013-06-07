using Portfotolio.DependencyInjection;
using Portfotolio.DependencyInjection.Unity;

namespace DependencyInjectionEngineFactory
{
    public class DependencyInjectionEngineFactory : IDependencyInjectionEngineFactory
    {
        public IDependencyEngine Create()
        {
            return new UnityDependencyEngine();
        }
    }
}
