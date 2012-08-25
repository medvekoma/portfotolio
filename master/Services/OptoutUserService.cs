using System;
using System.Collections.Generic;
using System.Threading;

namespace Services
{
    public class OptoutUserService : IOptoutUserService
    {
        private readonly IOptoutUserStore _optoutUserStore;

        public OptoutUserService(IOptoutUserStore optoutUserStore)
        {
            _optoutUserStore = optoutUserStore;
        }

        public SortedSet<string> GetOptedOutUserIds()
        {
            return _optoutUserStore.ReadUsers();
        }
    }
}
