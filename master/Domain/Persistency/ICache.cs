using System;

namespace Portfotolio.Domain.Persistency
{
    public interface ICache
    {
        void Add(string key, object value, DateTime expiration);
        T Get<T>(string key, T defaultValue);
        T Get<T>(string key);
    }
}