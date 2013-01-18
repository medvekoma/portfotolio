using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simplickr;
using Simplickr.Authentication;
using Simplickr.Configuration;
using Simplickr.Formatters;
using Simplickr.Parameters;

namespace SimplickrTests
{
    [TestClass]
    public class OAuthServiceTests
    {
        private readonly IOAuthService _oAuthService;
        private readonly IOAuthUrlService _oAuthUrlService;
        private readonly string _callbackUrl;
        private FlickrApi _flickrApi;

        public OAuthServiceTests()
        {
            _oAuthUrlService = new OAuthUrlService(new SimplickrConfigurationProvider());
            _oAuthService = new OAuthService(_oAuthUrlService, new HttpClient(), new QueryStringSerializer());

            _callbackUrl = "http://portfotolio.local/-oauth/authorize";

            ISimplickrFormatter simplickrFormatter = new SimplickrJsonFormatter();
            ISimplickrConfigurationProvider simplickrConfigurationProvider = new SimplickrConfigurationProvider();
            var flickrRequestBuilder = new FlickrRequestUrlProvider(simplickrFormatter, simplickrConfigurationProvider);
            IHttpClient httpClient = new HttpClient();
            _flickrApi = new FlickrApi(new FlickrApiInvoker(flickrRequestBuilder, httpClient, simplickrFormatter));
        }

        [TestMethod]
        public void ShouldGetRequestToken()
        {
            var requestToken = _oAuthService.GetRequestToken(_callbackUrl);

            Assert.IsNull(requestToken.OAuthProblem);
            Assert.IsTrue(requestToken.OAuthCallbackConfirmed);

            Console.WriteLine("token:  {0}\nsecret: {1}", requestToken.OAuthToken, requestToken.OAuthTokenSecret);

            var checkTokenResponse = _flickrApi.OAuthCheckToken(new OAuthCheckTokenParameters(requestToken.OAuthToken));
        }

        [TestMethod]
        public void ShouldGetUserAuthorizationUrl()
        {
            string callbackUrl = _callbackUrl;
            var requestToken = _oAuthService.GetRequestToken(callbackUrl);
            var userAuthorizationUrl = _oAuthUrlService.GetUserAuthorizationUrl(requestToken.OAuthToken);

            Console.WriteLine(userAuthorizationUrl);
            Console.WriteLine(requestToken.OAuthTokenSecret);
        }
    }
}
