using System.Collections.Generic;
using System.Globalization;
using NLog;

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

        private static readonly Logger Logger = LogManager.GetLogger("optout");

        public OptoutUserConfiguratorService(IOptoutUserStore optoutUserStore)
        {
            _optoutUserStore = optoutUserStore;
        }

        public void AddOptoutUser(string userId)
        {
            lock (_syncRoot)
            {
                var userIds = _optoutUserStore.ReadUsers() ?? new SortedSet<string>();
                if (userIds.Add(userId))
                {
                    _optoutUserStore.WriteUsers(userIds);
                    Logger.Log(LogLevel.Info, CultureInfo.InvariantCulture, "+ " + userId);
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
                    Logger.Log(LogLevel.Info, CultureInfo.InvariantCulture, "- " + userId);
                }
            }
        }
    }
}