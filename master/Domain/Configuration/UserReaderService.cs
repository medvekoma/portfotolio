namespace Portfotolio.Domain.Configuration
{
    public interface IUserReaderService
    {
        UserState GetUserState(string userId);
    }

    public class UserReaderService : IUserReaderService
    {
        private readonly IUserService _userService;

        public UserReaderService(IUserService userService)
        {
            _userService = userService;
        }

        public UserState GetUserState(string userId)
        {
            var optoutUserIds = _userService.GetOptoutUserIds();
            var isOptout = optoutUserIds.Contains(userId);
            if (isOptout)
                return UserState.Optout;

            var optinUserIds = _userService.GetOptinUserIds();
            var isOptin = optinUserIds.Contains(userId);
            return isOptin
                       ? UserState.Optin
                       : UserState.None;
        }
    }
}