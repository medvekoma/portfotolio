using FlickrNet;
using Portfotolio.Domain;
using Portfotolio.Utility.Extensions;

namespace Portfotolio.FlickrEngine
{
    public static class FlickrAuthenticationInfoExtensions
    {
        public static AuthenticationInfo AsAuthenticationInfo(this Auth auth)
        {
            var authenticationInfo = new AuthenticationInfo();
            if (auth != null)
            {
                var userId = auth.User.UserId;
                Person person = new Flickr().PeopleGetInfo(userId);
                var userAlias = person.PathAlias.IfNullOrEmpty(userId);
                authenticationInfo = new AuthenticationInfo(userId, userAlias, auth.User.UserName, auth.Token, null);
            }
            return authenticationInfo;
        }

        public static AuthenticationInfo AsAuthenticationInfo(this OAuthAccessToken accessToken)
        {
            var authenticationInfo = new AuthenticationInfo();
            if (accessToken != null)
            {
                var userId = accessToken.UserId;
                Person person = new Flickr().PeopleGetInfo(userId);
                authenticationInfo = new AuthenticationInfo(userId, person.PathAlias, accessToken.Username, accessToken.Token, accessToken.TokenSecret);
            }
            return authenticationInfo;
        }
    }
}