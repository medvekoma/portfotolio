namespace Portfotolio.Domain.Configuration
{
    public interface IUserWriterService
    {
        void Configure(string userId, UserState userState);
    }

    public class UserWriterService : IUserWriterService
    {
        private readonly IUserStoreService _userStoreService;

        public UserWriterService(IUserStoreService userStoreService)
        {
            _userStoreService = userStoreService;
        }

        public void Configure(string userId, UserState userState)
        {
            switch (userState)
            {
                case UserState.None:
                    _userStoreService.RemoveOptinUser(userId);
                    _userStoreService.RemoveOptoutUser(userId);
                    break;
                case UserState.Optin:
                    _userStoreService.RemoveOptoutUser(userId);
                    _userStoreService.AddOptintUser(userId);
                    break;
                case UserState.Optout:
                    _userStoreService.RemoveOptinUser(userId);
                    _userStoreService.AddOptoutUser(userId);
                    break;
            }
        }
    }
}