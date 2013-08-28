using System;
using FlickrNet;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.FlickrEngine
{
    [Obsolete("Don't use it yet, because it doesn't maintain permissions", true)]
    public class FlickrOAuthProvider : IOAuthProvider
    {
	    private readonly IAuthenticationStorage _authenticationStorage;

        public FlickrOAuthProvider(IAuthenticationStorage authenticationStorage)
        {
	        _authenticationStorage = authenticationStorage;
        }

	    public OAuthAuthorizationObject GetAuthorizationObject(string callbackUrl)
        {
            var flickr = new Flickr();
            var requestToken = flickr.OAuthGetRequestToken(callbackUrl);
            string authorizationUrl = flickr.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Read);

            return new OAuthAuthorizationObject(requestToken.TokenSecret, authorizationUrl);
        }

        public void Logout()
        {
            _authenticationStorage.Clear();
        }

        public AuthenticationInfo Authenticate(string oauthToken, string oauthTokenSecret, string verifier)
        {
            var accessToken = new Flickr().OAuthGetAccessToken(oauthToken, oauthTokenSecret, verifier);
            AuthenticationInfo authenticationInfo = accessToken.AsAuthenticationInfo();
            _authenticationStorage.SetAuthenticationInfo(authenticationInfo);

            return authenticationInfo;
        }

        public AuthenticationInfo GetAuthenticationInfo()
        {
            return _authenticationStorage.GetAuthenticationInfo();
        }
    }
}