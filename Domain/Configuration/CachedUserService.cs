using System;
using System.Collections.Generic;
using Portfotolio.Services.Caching;

namespace Portfotolio.Domain.Configuration
{
    public class CachedUserService : IUserService
    {
        private readonly IUserService _userService;
        private readonly ICacheProvider _cacheProvider;
        private readonly string _optoutCacheFileName;
        private readonly string _optinCacheFileName;

        public CachedUserService(IUserService userService, ICacheProvider cacheProvider, IUserStorePathProvider userStorePathProvider)
        {
            _userService = userService;
            _cacheProvider = cacheProvider;
            _optoutCacheFileName = userStorePathProvider.GetOptoutFileName();
            _optinCacheFileName = userStorePathProvider.GetOptinFileName();
        }

        public HashSet<string> GetOptoutUserIds()
        {
            return GetUserIds(CacheNames.OptoutUsers, () => _userService.GetOptoutUserIds(), _optoutCacheFileName);
        }

        public HashSet<string> GetOptinUserIds()
        {
            return GetUserIds(CacheNames.OptinUsers, () => _userService.GetOptinUserIds(), _optinCacheFileName);
        }

        private HashSet<string> GetUserIds(string cacheName, Func<HashSet<string>> getUserIds, string cacheFileName)
        {
            var cachedUserIds = _cacheProvider.Get<HashSet<string>>(cacheName);
            if (cachedUserIds != null)
                return cachedUserIds;

            var userIds = getUserIds();
            _cacheProvider.Set(cacheName, userIds, cacheFileName);
            return userIds;
        }
    }
}