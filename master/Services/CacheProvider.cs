using System.Runtime.Caching;

namespace Services
{
    public interface ICacheProvider
    {
        void Add(string name, object value, string dependentFilePath);
        T Get<T>(string name);
    }

    public class CacheProvider : ICacheProvider
    {
        public void Add(string name, object value, string dependentFilePath)
        {
            var fileChangeMonitor = new HostFileChangeMonitor(new[] { dependentFilePath});

            var cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.ChangeMonitors.Add(fileChangeMonitor);

            MemoryCache.Default.Set(name, value, cacheItemPolicy);
        }

        public T Get<T>(string name)
        {
            object value = MemoryCache.Default.Get(name) ?? default(T);
            return (T) value;
        }
    }
}