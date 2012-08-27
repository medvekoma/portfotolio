using System.Collections.Generic;
using Portfotolio.Services.Caching;

namespace Portfotolio.Domain.Configuration
{
    public class CachedOptoutUserService : IOptoutUserService
    {
        private readonly IOptoutUserService _optoutUserService;
        private readonly ICacheProvider _cacheProvider;
        private readonly string _cacheFileName;

        public CachedOptoutUserService(IOptoutUserService optoutUserService, ICacheProvider cacheProvider, IOptoutUserStorePathProvider optoutUserStorePathProvider)
        {
            _optoutUserService = optoutUserService;
            _cacheProvider = cacheProvider;
            _cacheFileName = optoutUserStorePathProvider.GetStorageFileName();
        }

        public SortedSet<string> GetOptedOutUserIds()
        {
            var cachedOptedOutUsers = _cacheProvider.Get<SortedSet<string>>(CacheNames.OptedOutUsers);
            if (cachedOptedOutUsers != null)
                return cachedOptedOutUsers;

            var optedOutUserIds = _optoutUserService.GetOptedOutUserIds();
            _cacheProvider.Set(CacheNames.OptedOutUsers, optedOutUserIds, _cacheFileName);
            return optedOutUserIds;
        }
    }
}