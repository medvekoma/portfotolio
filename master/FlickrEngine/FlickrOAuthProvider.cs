using System;
using FlickrNet;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.FlickrEngine
{
    [Obsolete("Don't use it yet, because it doesn't maintain permissions", true)]
    public class FlickrOAuthProvider : IOAuthProvider
    {
        private readonly IUserSession _userSession;

        public FlickrOAuthProvider(IUserSession userSession)
        {
            _userSession = userSession;
        }

        public OAuthAuthorizationObject GetAuthorizationObject(string callbackUrl)
        {
            var requestToken = FlickrFactory.UnauthenticatedFlickr.OAuthGetRequestToken(callbackUrl);
            string authorizationUrl = FlickrFactory.UnauthenticatedFlickr.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Read);

            return new OAuthAuthorizationObject(requestToken.TokenSecret, authorizationUrl);
        }

        public void Logout()
        {
            _userSession.SetAuthenticationInfo(new AuthenticationInfo());
        }

        public AuthenticationInfo Authenticate(string oauthToken, string oauthTokenSecret, string verifier)
        {
            var accessToken = FlickrFactory.UnauthenticatedFlickr.OAuthGetAccessToken(oauthToken, oauthTokenSecret, verifier);
            AuthenticationInfo authenticationInfo = accessToken.AsAuthenticationInfo();
            _userSession.SetAuthenticationInfo(authenticationInfo);

            return authenticationInfo;
        }

        public AuthenticationInfo GetAuthenticationInfo()
        {
            return _userSession.GetAuthenticationInfo();
        }
    }
}