using System.Collections.Generic;

namespace Portfotolio.Domain.Configuration
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
