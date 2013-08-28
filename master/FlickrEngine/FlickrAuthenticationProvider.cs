using FlickrNet;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.FlickrEngine
{
    public class FlickrAuthenticationProvider : IAuthenticationProvider
    {
	    private readonly IAuthenticationStorage _authenticationStorage;

        public FlickrAuthenticationProvider(IAuthenticationStorage authenticationStorage)
        {
	        _authenticationStorage = authenticationStorage;
        }

	    public string GetLoginUrl()
        {
            string loginUrl = new Flickr().AuthCalcWebUrl(AuthLevel.Read);
            return loginUrl;
        }

        public void Logout()
        {
            _authenticationStorage.Clear();
        }

        public AuthenticationInfo Authenticate(object parameter)
        {
            var frob = (string) parameter;
            Auth auth = new Flickr().AuthGetToken(frob);
            var authenticationInfo = auth.AsAuthenticationInfo();
            _authenticationStorage.SetAuthenticationInfo(authenticationInfo);
            
            return authenticationInfo;
        }

        public AuthenticationInfo GetAuthenticationInfo()
        {
            return _authenticationStorage.GetAuthenticationInfo();
        }
    }
}