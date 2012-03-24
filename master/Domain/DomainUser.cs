namespace Portfotolio.Domain
{
    public sealed class DomainUser
    {
        public string UserId { get; private set; }
        public string UserName { get; private set; }
        public string UserAlias { get; private set; }
        public bool IsAcceptedUserName { get; private set; }

        public DomainUser(string userId, string userName, string userAlias, bool isAcceptedUserName)
        {
            UserId = userId;
            UserName = userName;
            UserAlias = userAlias;
            IsAcceptedUserName = isAcceptedUserName;
        }

        public DomainUser ToAcceptedUser()
        {
            return new DomainUser(UserId, UserName, UserAlias, true);
        }
    }
}