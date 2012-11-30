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
        private readonly Flickr _flickr;

        public FlickrOAuthProvider(IUserSession userSession)
        {
            _userSession = userSession;
            _flickr = new Flickr();
        }

        public OAuthAuthorizationObject GetAuthorizationObject(string callbackUrl)
        {
            var requestToken = _flickr.OAuthGetRequestToken(callbackUrl);
            string authorizationUrl  = _flickr.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Read);

            return new OAuthAuthorizationObject(requestToken.TokenSecret, authorizationUrl);
        }

        public void Logout()
        {
            _userSession.SetAuthenticationInfo(new AuthenticationInfo());
        }

        public AuthenticationInfo Authenticate(string oauthToken, string oauthTokenSecret, string verifier)
        {
            var accessToken = _flickr.OAuthGetAccessToken(oauthToken, oauthTokenSecret, verifier);
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