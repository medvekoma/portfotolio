using System;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;
using Services;

namespace Portfotolio.FlickrEngine
{
    public class CachedUserEngine : IUserEngine
    {
        private readonly IUserEngine _decoratedUserEngine;
        private readonly ICache _cache;

        public CachedUserEngine(IUserEngine decoratedUserEngine, ICache cache)
        {
            _decoratedUserEngine = decoratedUserEngine;
            _cache = cache;
        }

        public DomainUser GetUser(string userIdentifier)
        {
            return CacheHelper(
                GetCacheKey(userIdentifier), 
                domainUser => GetCacheKey(domainUser.UserAlias), 
                () => _decoratedUserEngine.GetUser(userIdentifier), 60);
        }

        #region helpers

        private static string GetCacheKey(string userIdentifier)
        {
            return CacheNames.DomainUser + userIdentifier;
        }

        private TValue CacheHelper<TValue>(string key, Func<TValue, string> keyGetter, Func<TValue> valueGetter, double minutesToCacheFor)
            where TValue : class
        {
            var cachedValue = _cache.Get<TValue>(key);
            if (cachedValue != null)
                return cachedValue;

            var value = valueGetter.Invoke();
            key = keyGetter.Invoke(value);
            _cache.Add(key, value, DateTime.Now.AddMinutes(minutesToCacheFor));
            return value;
        }

        #endregion

    }
}