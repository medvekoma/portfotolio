using System.Collections.Generic;
using Portfotolio.Services.Logging;

namespace Portfotolio.Domain.Configuration
{
    public interface IOptoutUserConfiguratorService
    {
        void AddOptoutUser(string userId);
        void RemoveOptoutUser(string userId);
    }

    public class OptoutUserConfiguratorService : IOptoutUserConfiguratorService
    {
        private readonly IOptoutUserStore _optoutUserStore;
        private readonly object _syncRoot = new object();
        private readonly ILogger _logger;

        public OptoutUserConfiguratorService(IOptoutUserStore optoutUserStore, ILoggerFactory loggerFactory)
        {
            _optoutUserStore = optoutUserStore;
            _logger = loggerFactory.GetLogger("optout");
        }

        public void AddOptoutUser(string userId)
        {
            lock (_syncRoot)
            {
                var userIds = _optoutUserStore.ReadUsers() ?? new SortedSet<string>();
                if (userIds.Add(userId))
                {
                    _optoutUserStore.WriteUsers(userIds);
                    _logger.Info("+ " + userId);
                }
            }
        }

        public void RemoveOptoutUser(string userId)
        {
            lock (_syncRoot)
            {
                var userIds = _optoutUserStore.ReadUsers() ?? new SortedSet<string>();
                if (userIds.Remove(userId))
                {
                    _optoutUserStore.WriteUsers(userIds);
                    _logger.Info("- " + userId);
                }
            }
        }
    }
}