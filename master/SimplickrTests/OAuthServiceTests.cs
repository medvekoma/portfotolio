using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simplickr;
using Simplickr.Authentication;
using Simplickr.Configuration;

namespace SimplickrTests
{
    [TestClass]
    public class OAuthServiceTests
    {
        private readonly IOAuthService _oAuthService;
        private readonly IOAuthUrlService _oAuthUrlService;
        private readonly string _callbackUrl;

        public OAuthServiceTests()
        {
            _oAuthUrlService = new OAuthUrlService(new SimplickrConfigurationProvider());
            _oAuthService = new OAuthService(_oAuthUrlService, new HttpClient(), new OAuthResponseProcessor());

            _callbackUrl = "http://portfotolio.local/-oauth/authorize";
        }

        [TestMethod]
        public void ShouldGetRequestToken()
        {
            var oAuthResponse = _oAuthService.GetRequestToken(_callbackUrl);

            Assert.IsNull(oAuthResponse.Problem);
            Assert.IsTrue(oAuthResponse.CallbackConfirmed);

            Console.WriteLine("token:  {0}\nsecret: {1}", oAuthResponse.Token, oAuthResponse.TokenSecret);
        }

        [TestMethod]
        public void ShouldGetUserAuthorizationUrl()
        {
            string callbackUrl = _callbackUrl;
            var oAuthResponse = _oAuthService.GetRequestToken(callbackUrl);
            var userAuthorizationUrl = _oAuthUrlService.GetUserAuthorizationUrl(oAuthResponse.Token);

            Console.WriteLine(userAuthorizationUrl);
            Console.WriteLine(oAuthResponse.TokenSecret);
        }
    }
}
