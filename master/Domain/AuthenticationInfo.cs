namespace Portfotolio.Domain
{
    public sealed class AuthenticationInfo
    {
		public bool IsAuthenticated { get; set; }
		public string UserId { get; set; }
		public string UserAlias { get; set; }
		public string UserName { get; set; }
		public string Token { get; set; }

        private AuthenticationInfo(bool isAuthenticated, string userId, string userAlias, string userName, string token)
        {
            IsAuthenticated = isAuthenticated;
            UserId = userId;
            UserAlias = userAlias;
            UserName = userName;
            Token = token;
        }

		public AuthenticationInfo(string userId, string userAlias, string userName, string token)
			: this(true, userId, userAlias, userName, token)
		{
		}

		public AuthenticationInfo()
		{
		}

#if ISerializable
		public AuthenticationInfo(SerializationInfo info, StreamingContext context)
		{
			IsAuthenticated = info.GetBoolean("IsAuthenticated");
			UserId = info.GetString("UserId");
			UserAlias = info.GetString("UserAlias");
			UserName = info.GetString("UserName");
			Token = info.GetString("Token");
			TokenSecret = info.GetString("TokenSecret");
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
#endif
	}
}