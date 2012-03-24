using FlickrNet;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.FlickrEngine
{
    public class FlickrAuthenticationProvider : IAuthenticationProvider
    {
        private readonly Flickr _flickr;
        private readonly IUserSession _userSession;

        public FlickrAuthenticationProvider(IUserSession userSession)
        {
            _flickr = new Flickr();
            _userSession = userSession;
        }

        public string GetLoginUrl()
        {
            string loginUrl = _flickr.AuthCalcWebUrl(AuthLevel.Read);
            return loginUrl;
        }

        public void Logout()
        {
            _userSession.SetAuthenticationInfo(new AuthenticationInfo());
        }

        public AuthenticationInfo Authenticate(object authenticationObject)
        {
            var frob = (string) authenticationObject;
            Auth auth = _flickr.AuthGetToken(frob);
            var authenticationInfo = auth.AsAuthenticationInfo();
            _userSession.SetAuthenticationInfo(authenticationInfo);
            
            return authenticationInfo;
        }

        public AuthenticationInfo GetAuthenticationInfo()
        {
            return _userSession.GetAuthenticationInfo();
        }
    }
}