﻿using System;
using Portfotolio.Domain;
using Portfotolio.Services.Caching;

namespace Portfotolio.FlickrEngine
{
    public class CachedUserEngine : IUserEngine
    {
        private readonly IUserEngine _userEngine;
        private readonly ICacheProvider _cacheProvider;

        public CachedUserEngine(IUserEngine userEngine, ICacheProvider cacheProvider)
        {
            _userEngine = userEngine;
            _cacheProvider = cacheProvider;
        }

        public DomainUser GetUser(string userIdentifier)
        {
            return CacheHelper(
                GetCacheKey(userIdentifier), 
                domainUser => GetCacheKey(domainUser.UserAlias), 
                () => _userEngine.GetUser(userIdentifier), 
                10);
        }

        #region helpers

        private static string GetCacheKey(string userIdentifier)
        {
            return CacheNames.DomainUser + userIdentifier;
        }

        private TValue CacheHelper<TValue>(string key, Func<TValue, string> keyGetter, Func<TValue> valueGetter, double minutesToCacheFor)
            where TValue : class
        {
            var cachedValue = _cacheProvider.Get<TValue>(key);
            if (cachedValue != null)
                return cachedValue;

            var value = valueGetter.Invoke();
            key = keyGetter.Invoke(value);
            _cacheProvider.Set(key, value, DateTime.Now.AddMinutes(minutesToCacheFor));
            return value;
        }

        #endregion

    }
}