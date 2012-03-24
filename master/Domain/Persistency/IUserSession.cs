using System.Runtime.Serialization;

namespace Portfotolio.Domain.Persistency
{
    public interface IUserSession
    {
        void SetValue(string key, object value);
        T GetValue<T>(string key);
    }
}