using System;
using System.Web;
using System.Web.Mvc;
using Simplickr;
using Simplickr.Authentication;
using Simplickr.Configuration;

namespace Portfotolio.Site4.Controllers
{
    public class OAuthController : Controller
    {
        private readonly IOAuthService _oAuthService;
        private readonly IOAuthUrlService _oAuthUrlService;

        public OAuthController()
        {
            _oAuthUrlService = new OAuthUrlService(new SimplickrConfigurationProvider());
            IHttpClient httpClient = new HttpClient();
            IOAuthResponseProcessor oAuthResponseProcessor = new OAuthResponseProcessor();
            _oAuthService = new OAuthService(_oAuthUrlService, httpClient, oAuthResponseProcessor);
        }

        public ActionResult Authorize()
        {
            var uri = HttpContext.Request.Url;
            if (uri == null)
                throw new ApplicationException("Request Url is null");

            string callbackUrl = Url.Action("callback", "oauth");
            var applicationHome = uri.AbsoluteUri.Replace(uri.AbsolutePath, "");
            callbackUrl = applicationHome + callbackUrl;
            var requestToken = _oAuthService.GetRequestToken(callbackUrl);

            var userAuthorizationUrl = _oAuthUrlService.GetUserAuthorizationUrl(requestToken.Token);
            TempData["OAuthTokenSecret"] = requestToken.TokenSecret;

            return Redirect(userAuthorizationUrl);
        }

        public ActionResult Callback(string oauth_token, string oauth_verifier)
        {
            var tokenSecret = (string) TempData["OAuthTokenSecret"];
            string accessToken = _oAuthService.GetAccessToken(oauth_token, tokenSecret, oauth_verifier);
            return Content(accessToken);
        }
    }
}
