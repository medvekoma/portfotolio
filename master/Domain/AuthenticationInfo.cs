using System.Runtime.Serialization;

namespace Portfotolio.Domain
{
    public struct AuthenticationInfo : ISerializable
    {
        public bool IsAuthenticated;
        public string UserId;
        public string UserAlias;
        public string UserName;
        public string Token;

        public AuthenticationInfo(string userId, string userAlias, string userName, string token)
        {
            IsAuthenticated = true;
            UserId = userId;
            UserAlias = userAlias;
            UserName = userName;
            Token = token;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("IsAuthenticated", IsAuthenticated);
            info.AddValue("UserId", UserId);
            info.AddValue("UserAlias", UserAlias);
            info.AddValue("UserName", UserName);
            info.AddValue("Token", Token);
        }
    }
}