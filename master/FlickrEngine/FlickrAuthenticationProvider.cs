using FlickrNet;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.FlickrEngine
{
    public class FlickrAuthenticationProvider : IAuthenticationProvider
    {
        private readonly IUserSession _userSession;

        public FlickrAuthenticationProvider(IUserSession userSession)
        {
            _userSession = userSession;
        }

        public string GetLoginUrl()
        {
            string loginUrl = new Flickr().AuthCalcWebUrl(AuthLevel.Read);
            return loginUrl;
        }

        public void Logout()
        {
            _userSession.RemoveAuthenticationInfo();
        }

        public AuthenticationInfo Authenticate(object parameter)
        {
            var frob = (string) parameter;
            Auth auth = new Flickr().AuthGetToken(frob);
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