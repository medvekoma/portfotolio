using System;
using System.Web;
using System.Web.Mvc;
using Portfotolio.Site4.Models;
using Simplickr;
using Simplickr.Authentication;
using Simplickr.Configuration;
using Simplickr.Formatters;
using Simplickr.Parameters;

namespace Portfotolio.Site4.Controllers
{
    public class OAuthController : Controller
    {
        private readonly IOAuthService _oAuthService;
        private readonly IOAuthUrlService _oAuthUrlService;
        private readonly FlickrApi _flickrApi;

        public OAuthController()
        {
            _oAuthUrlService = new OAuthUrlService(new SimplickrConfigurationProvider());
            IHttpClient httpClient = new HttpClient();
            _oAuthService = new OAuthService(_oAuthUrlService, httpClient, new QueryStringSerializer());

            ISimplickrFormatter simplickrFormatter = new SimplickrJsonFormatter();
            ISimplickrConfigurationProvider simplickrConfigurationProvider = new SimplickrConfigurationProvider();
            var flickrRequestBuilder = new FlickrRequestUrlProvider(simplickrFormatter, simplickrConfigurationProvider);
            _flickrApi = new FlickrApi(new FlickrApiInvoker(flickrRequestBuilder, httpClient, simplickrFormatter));
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

            var userAuthorizationUrl = _oAuthUrlService.GetUserAuthorizationUrl(requestToken.OAuthToken);
            TempData["OAuthTokenSecret"] = requestToken.OAuthTokenSecret;

            return Redirect(userAuthorizationUrl);
        }

        public ActionResult Callback(string oauth_token, string oauth_verifier)
        {
            var tokenSecret = (string) TempData["OAuthTokenSecret"];
            Session["OAuthToken"] = oauth_token;

            var accessToken = _oAuthService.GetAccessToken(oauth_token, tokenSecret, oauth_verifier);

            var parameters = new OAuthCheckTokenParameters(oauth_token);
            var checkTokenResult = _flickrApi.OAuthCheckToken(parameters);
            ViewBag.CheckTokenResponse = checkTokenResult;

            var getPhotoParameters = new GetPhotosParameters(userId: "27725019@N00")
                .PerPage(10);
            var flickrPhotosResult = _flickrApi.PeopleGetPhotos(getPhotoParameters);

            var oAuthModel = new OAuthModel
                {
                    AccessToken = accessToken, 
                    CheckTokenResult = checkTokenResult,
                    Photos = flickrPhotosResult,
                };

            return View(oAuthModel);
        }
    }
}
