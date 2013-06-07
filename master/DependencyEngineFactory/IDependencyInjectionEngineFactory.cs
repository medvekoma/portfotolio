using Portfotolio.DependencyInjection;

namespace DependencyInjectionEngineFactory
{
    public interface IDependencyInjectionEngineFactory
    {
        IDependencyEngine Create();
    }
}