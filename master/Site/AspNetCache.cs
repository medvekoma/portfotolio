using System;
using System.Web;
using System.Web.Caching;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site
{
    public class AspNetCache : ICache
    {
        public void Add(string key, object value, DateTime expiration)
        {
            HttpContext.Current.Cache.Add(key, value, null, expiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }

        public T Get<T>(string key, T defaultValue)
        {
            object value = HttpContext.Current.Cache[key];
            try
            {
                return (T) value;
            }
            catch
            {
                return defaultValue;
            }
        }

        public T Get<T>(string key)
        {
            return Get(key, default(T));
        }
    }
}