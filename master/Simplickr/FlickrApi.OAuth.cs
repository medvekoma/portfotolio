using Simplickr.Parameters;
using Simplickr.Response;

namespace Simplickr
{
    public partial class FlickrApi
    {
        public OAuthCheckTokenResponse OAuthCheckToken(OAuthCheckTokenParameters parameters)
        {
            return _flickrApiInvoker.Invoke<OAuthCheckTokenResponse>("flickr.auth.oauth.checkToken", parameters, true);
        }
    }
}