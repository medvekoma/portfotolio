using Portfotolio.DependencyInjection.Unity;

namespace Portfotolio.DependencyInjection.EngineFactory
{
    public class DependencyInjectionEngineFactory
    {
        public IDependencyEngine Create()
        {
            return new UnityDependencyEngine();
        }
    }
}

