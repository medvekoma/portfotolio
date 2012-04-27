using System;
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
        public string TokenSecret;

        public AuthenticationInfo(string userId, string userAlias, string userName, string token, string tokenSecret)
        {
            IsAuthenticated = true;
            UserId = userId;
            UserAlias = userAlias;
            UserName = userName;
            Token = token;
            TokenSecret = tokenSecret;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("IsAuthenticated", IsAuthenticated);
            info.AddValue("UserId", UserId);
            info.AddValue("UserAlias", UserAlias);
            info.AddValue("UserName", UserName);
            info.AddValue("Token", Token);
            info.AddValue("TokenSecret", TokenSecret);
        }
    }
}