using System;
using System.Runtime.Caching;

namespace Portfotolio.Services.Caching
{
    public interface ICacheProvider
    {
        void Set(string name, object value, DateTimeOffset dateTimeOffset);
        void Set(string name, object value, string dependentFilePath);
        T Get<T>(string name);
        long GetCacheSize();
    }

    public class CacheProvider : ICacheProvider
    {
        public void Set(string name, object value, DateTimeOffset dateTimeOffset)
        {
            MemoryCache.Default.Set(name, value, dateTimeOffset);
        }

        public void Set(string name, object value, string dependentFilePath)
        {
            var fileChangeMonitor = CreateFileChangeMonitor(dependentFilePath);

            var cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.ChangeMonitors.Add(fileChangeMonitor);

            MemoryCache.Default.Set(name, value, cacheItemPolicy);
        }

        public T Get<T>(string name)
        {
            object value = MemoryCache.Default.Get(name) ?? default(T);
            return (T) value;
        }

        public long GetCacheSize()
        {
            return  MemoryCache.Default.GetCount();
        }

        private ChangeMonitor CreateFileChangeMonitor(string path)
        {
            var hostFileChangeMonitor = new HostFileChangeMonitor(new[] {path});
            return hostFileChangeMonitor;
        }
    }
}