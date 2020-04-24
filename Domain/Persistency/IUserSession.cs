using System.Runtime.Serialization;

namespace Portfotolio.Domain.Persistency
{
    public interface IUserSession
    {
        void SetValue(string key, string value);
        string GetValue(string key);
    }
}