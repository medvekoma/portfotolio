using Simplickr.Authentication;
using Simplickr.Response;

namespace Portfotolio.Site4.Models
{
    public class OAuthModel
    {
        public OAuthAccessToken AccessToken { get; set; }
        public OAuthCheckTokenResponse CheckTokenResult { get; set; }
        public FlickrPhotosResponse Photos { get; set; }
    }
}