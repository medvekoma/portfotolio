using System.Collections.Generic;

namespace Portfotolio.Domain.Configuration
{
    public class UserService : IUserService
    {
        private readonly IUserStore _userStore;

        public UserService(IUserStore userStore)
        {
            _userStore = userStore;
        }

        public HashSet<string> GetOptoutUserIds()
        {
            return _userStore.ReadOptoutUsers();
        }

        public HashSet<string> GetOptinUserIds()
        {
            return _userStore.ReadOptinUsers();
        }
    }
}
