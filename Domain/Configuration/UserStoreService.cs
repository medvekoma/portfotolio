using System;
using System.Collections.Generic;
using Portfotolio.Services.Logging;

namespace Portfotolio.Domain.Configuration
{
    public interface IUserStoreService
    {
        bool AddOptoutUser(string userId);
        bool RemoveOptoutUser(string userId);
        bool AddOptintUser(string userId);
        bool RemoveOptinUser(string userId);
    }

    public class UserStoreService : IUserStoreService
    {
        private readonly IUserStore _userStore;
        private readonly object _syncRoot = new object();

        public UserStoreService(IUserStore userStore)
        {
            _userStore = userStore;
        }

        public bool AddOptoutUser(string userId)
        {
            return ProcessUser(userId,
                () => _userStore.ReadOptoutUsers(),
                ids => _userStore.WriteOptoutUsers(ids),
                (ids, id) => ids.Add(id));
        }

        public bool RemoveOptoutUser(string userId)
        {
            return ProcessUser(userId, 
                () => _userStore.ReadOptoutUsers(), 
                ids => _userStore.WriteOptoutUsers(ids), 
                (ids, id) => ids.Remove(id));
        }

        public bool AddOptintUser(string userId)
        {
            return ProcessUser(userId,
                () => _userStore.ReadOptinUsers(),
                ids => _userStore.WriteOptinUsers(ids),
                (ids, id) => ids.Add(id));
        }

        public bool RemoveOptinUser(string userId)
        {
            return ProcessUser(userId,
                () => _userStore.ReadOptinUsers(),
                ids => _userStore.WriteOptinUsers(ids),
                (ids, id) => ids.Remove(id));
        }

        private bool ProcessUser(string userId, Func<HashSet<string>> reader, Action<HashSet<string>> writer, Func<HashSet<string>, string, bool> processor)
        {
            lock (_syncRoot)
            {
                var userIds = reader() ?? new HashSet<string>();
                var result = processor(userIds, userId);
                if (result)
                {
                    writer(userIds);
                }
                return result;
            }
        }
    }
}